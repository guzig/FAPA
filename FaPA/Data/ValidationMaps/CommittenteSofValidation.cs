using FaPA.Core;
using FaPA.DomainServices;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Validator.Cfg.Loquacious;
using NHibernate.Validator.Engine;

namespace FaPA.Data.ValidationMaps
{
    public class CommittenteSofValidation : ValidationDef<Core.Committente>
    {
        //note:use of ValidateInstance.By because : "NHV was designed more thinking about properties-constraints than Business-Rules"
        //see:http://fabiomaulo.blogspot.it/2010/01/nhibernatevalidator-changing-validation.html
        public CommittenteSofValidation()
        {
            ValidateInstance.By( ValidateInstanc );

            //Define( l => l.Comune ).NotNullableAndNotEmpty().WithMessage( "Specificare un comune" );
            //Define( l => l.Provincia ).NotNullableAndNotEmpty().WithMessage( "Specificare la provincia " );
            //Define( l => l.Cap ).NotNullableAndNotEmpty().WithMessage( "Specificare la provincia " );
            //Define( f => f.Indirizzo ).NotNullable().WithMessage( "Specificare indirizzo" );
            //Define( f => f.Civico ).NotNullable().WithMessage( "Specificare il civico" );
            //Define( f => f.Nazione ).NotNullable().WithMessage( "Specificare la naizone" );

        }

        private static bool ValidateInstanc( Committente anag, IConstraintValidatorContext context )
        {
            bool isValid = true;
            if ( !string.IsNullOrWhiteSpace( anag.PIva ) || !string.IsNullOrWhiteSpace( anag.Denominazione ) )
            {
                if ( anag.PIva.Length != 11 )
                {
                    context.AddInvalid<Committente, string>( "P.IVA non valida", p => p.Denominazione );
                    isValid = false;
                }

                if ( anag.CodiceFiscale.Length != 11 )
                {
                    context.AddInvalid<Committente, string>( "Codice fiscale non valido", p => p.Denominazione );
                    isValid = false;
                }

                if ( string.IsNullOrWhiteSpace( anag.Denominazione ) )
                {
                    context.AddInvalid<Committente, string>( "Denominazione non valida", p => p.Denominazione );
                    isValid = false;
                }
                if ( string.IsNullOrWhiteSpace( anag.PIva ) )
                {
                    context.AddInvalid<Committente, string>( "P.Iva non valida", p => p.PIva );
                    isValid = false;
                }              
            }
            else
            {
                if ( string.IsNullOrWhiteSpace( anag.Cognome ) )
                {
                    context.AddInvalid<Committente, string>( "Cognome non valido", p => p.Cognome );
                    isValid = false;
                }

                if ( string.IsNullOrWhiteSpace( anag.Nome ) )
                {
                    context.AddInvalid<Committente, string>( "Nome non valido", p => p.Nome );
                    isValid = false;
                }

                if ( string.IsNullOrWhiteSpace( anag.CodiceFiscale ) )
                {
                    context.AddInvalid<Committente, string>( "CodiceFiscale non valido", p => p.CodiceFiscale );
                    isValid = false;
                }
            }

            if ( !isValid  ) return false;

            if ( string.IsNullOrWhiteSpace( anag.CodiceFiscale ) || IsUnique( anag ) ) return true;

            const string error = "Questo codice fiscale esiste già";
            context.AddInvalid<Committente, string>( error, p => p.CodiceFiscale );

            return false;
        }

        private static bool IsUnique( Committente anag  )
        {
            object isLock = 0;
            int result;
            lock ( isLock )
            {
                using ( var tx = NHibernateStaticContainer.Session.BeginTransaction() )
                {
                    result = NHibernateStaticContainer.Session.QueryOver<Committente>().
                        Where( f => f.CodiceFiscale == anag.CodiceFiscale ).And( f => f.Id != anag.Id ).
                        Select( Projections.Count<Committente>( f => f.Id ) ).
                        Cacheable().CacheMode( CacheMode.Normal ).
                        FutureValue<int>().Value;

                    tx.Commit();
                }
            }

            return result == 0;
        }

    }
}