using FaPA.Core;
using FaPA.DomainServices;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Validator.Cfg.Loquacious;
using NHibernate.Validator.Engine;

namespace FaPA.Data.ValidationMaps
{
    public class AnagraficaSofValidation : ValidationDef<Anagrafica>
    {
        //note:use of ValidateInstance.By because : "NHV was designed more thinking about properties-constraints than Business-Rules"
        //see:http://fabiomaulo.blogspot.it/2010/01/nhibernatevalidator-changing-validation.html
        public AnagraficaSofValidation()
        {
            ValidateInstance.By( ValidateInstanc );

            Define( l => l.Comune ).NotNullableAndNotEmpty().WithMessage( "Specificare un comune" ).And.MaxLength( 60 );
            Define( l => l.Provincia ).NotNullableAndNotEmpty().WithMessage( "Specificare la provincia " ).And.MaxLength( 2 );
            Define( l => l.Cap ).NotNullableAndNotEmpty().WithMessage( "Specificare la provincia " ).And.MaxLength( 5 );
            Define( f => f.Indirizzo ).NotNullable().WithMessage( "Specificare indirizzo" ).And.MaxLength( 60 );
            Define( f => f.Civico ).NotNullable().WithMessage( "Specificare il civico" ).And.MaxLength( 8 );
            Define( f => f.Nazione ).NotNullable().WithMessage( "Specificare la naizone" );
            Define(f => f.Email).IsEmail();
            Define( f => f.Pec ).IsEmail();
            Define(f => f.CodiceFiscale).IsCodiceFiscale();
            Define(f => f.PIva).IsPartitaIva();
            Define( f => f.Cognome ).MaxLength( 60 );
            Define( f => f.Nome ).MaxLength( 60 );
            Define( f => f.Denominazione ).MaxLength( 80 );
            Define( f => f.Tel ).MinLength( 5 ).And.MaxLength( 12 );
            Define( f => f.Fax ).MinLength( 5 ).And.MaxLength( 12 );
            Define( f => f.CodUfficioPa ).MinLength( 6 ).And.MaxLength( 6 );
            Define( f => f.RifAmministrazione ).MaxLength( 80 );
        }

        private static bool ValidateInstanc( Anagrafica anag, IConstraintValidatorContext context )
        {
            bool isValid = true;
            if ( !string.IsNullOrWhiteSpace( anag.PIva ) || !string.IsNullOrWhiteSpace( anag.Denominazione ) )
            {
                if ( string.IsNullOrWhiteSpace( anag.PIva )  || anag.PIva.Length != 11 )
                {
                    context.AddInvalid<Anagrafica, string>( "P.IVA non valida", p => p.Denominazione );
                    isValid = false;
                }

                //if ( !string.IsNullOrWhiteSpace( anag.CodiceFiscale) && anag.CodiceFiscale.Length != 16 )
                //{
                //    context.AddInvalid<Anagrafica, string>( "Codice fiscale non valido", p => p.Denominazione );
                //    isValid = false;
                //}

                if ( string.IsNullOrWhiteSpace( anag.Denominazione ) )
                {
                    context.AddInvalid<Anagrafica, string>( "Denominazione non valida", p => p.Denominazione );
                    isValid = false;
                }
                if ( string.IsNullOrWhiteSpace( anag.PIva ) )
                {
                    context.AddInvalid<Anagrafica, string>( "P.Iva non valida", p => p.PIva );
                    isValid = false;
                }              
            }
            else
            {
                if ( string.IsNullOrWhiteSpace( anag.Cognome ) )
                {
                    context.AddInvalid<Anagrafica, string>( "Cognome non valido", p => p.Cognome );
                    isValid = false;
                }

                if ( string.IsNullOrWhiteSpace( anag.Nome ) )
                {
                    context.AddInvalid<Anagrafica, string>( "Nome non valido", p => p.Nome );
                    isValid = false;
                }

                if ( string.IsNullOrWhiteSpace( anag.CodiceFiscale ) )
                {
                    context.AddInvalid<Anagrafica, string>( "CodiceFiscale non valido", p => p.CodiceFiscale );
                    isValid = false;
                }
            }

            if ( !isValid  ) return false;

            if ( string.IsNullOrWhiteSpace( anag.CodiceFiscale ) || IsUnique( anag ) ) return true;

            const string error = "Questo codice fiscale esiste già";
            context.AddInvalid<Anagrafica, string>( error, p => p.CodiceFiscale );

            return false;
        }

        private static bool IsUnique( Anagrafica anag  )
        {
            object isLock = 0;
            int result;
            lock ( isLock )
            {
                using ( var tx = NHibernateStaticContainer.Session.BeginTransaction() )
                {
                    result = NHibernateStaticContainer.Session.QueryOver<Anagrafica>().
                        Where( f => f.CodiceFiscale == anag.CodiceFiscale ).And( f => f.Id != anag.Id ).
                        Select( Projections.Count<Anagrafica>( f => f.Id ) ).
                        Cacheable().CacheMode( CacheMode.Normal ).
                        FutureValue<int>().Value;

                    tx.Commit();
                }
            }

            return result == 0;
        }

    }
}