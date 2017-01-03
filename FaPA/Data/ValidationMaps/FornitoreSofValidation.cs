using System;
using FaPA.Core;
using FaPA.DomainServices;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Validator.Cfg.Loquacious;
using NHibernate.Validator.Engine;

namespace FaPA.Data.ValidationMaps
{
    public class FornitoreSofValidation : ValidationDef<Core.Fornitore>
    {
        //note:use of ValidateInstance.By because : "NHV was designed more thinking about properties-constraints than Business-Rules"
        //see:http://fabiomaulo.blogspot.it/2010/01/nhibernatevalidator-changing-validation.html
        public FornitoreSofValidation()
        {
            ValidateInstance.By( IsUnique );

            Define( l => l.PIva ).NotNullableAndNotEmpty().WithMessage( "Specificare la partita IVA" );
            Define( l => l.Denominazione ).NotNullableAndNotEmpty().WithMessage( "Specificare la ragione sociale" );

            Define( l => l.Comune ).NotNullableAndNotEmpty().WithMessage( "Specificare un comune" );
            Define( l => l.Provincia ).NotNullableAndNotEmpty().WithMessage( "Specificare la provincia " );
            Define( l => l.Cap ).NotNullableAndNotEmpty().WithMessage( "Specificare la provincia " );
            Define( f => f.Indirizzo ).NotNullable().WithMessage( "Specificare indirizzo" );
            Define( f => f.Civico ).NotNullable().WithMessage( "Specificare il civico" );
            Define( f => f.Nazione ).NotNullable().WithMessage( "Specificare la naizone" );
        }

        private static bool IsUnique( Fornitore anag, IConstraintValidatorContext context )
        {
            if ( string.IsNullOrWhiteSpace( anag.Denom ) || string.IsNullOrWhiteSpace( anag.PIva ) )
                return false;

            object isLock = 0;
            int result;
            lock ( isLock )
            {
                using ( var tx = NHibernateStaticContainer.Session.BeginTransaction() )
                {
                    result = NHibernateStaticContainer.Session.QueryOver<Anagrafica>().
                        Where( f => f.PIva == anag.PIva ).
                        And( f => f.Denominazione == anag.Denominazione ).And( f => f.Id != anag.Id ).
                        Select( Projections.Count<Fornitore>( f => f.Id ) ).Cacheable().CacheMode( CacheMode.Normal )
                        .FutureValue<int>().Value;

                    tx.Commit();
                }
            }

            if ( result == 0 ) return true;

            const string error = "P.Iva o denomniazione non sono unici";
            context.AddInvalid<Fornitore, string>( error, p => p.PIva );
            context.AddInvalid<Fornitore, string>( error, p => p.Denominazione );

            return false;

        }
    }
}