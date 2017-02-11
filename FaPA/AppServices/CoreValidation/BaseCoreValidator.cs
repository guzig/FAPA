using System.Collections.Generic;
using System.Linq;

namespace FaPA.AppServices.CoreValidation
{
    public abstract class BaseCoreValidator : ICoreValidator
    {
      
        public abstract IDictionary<string, IEnumerable<string>> GetValidationErrors(object instance);

        public virtual IDictionary<string, IEnumerable<string>> GetValidationErrors(string columnName, object instance)
        {
            var errors = GetValidationErrors(instance);

            return new Dictionary<string, IEnumerable<string>>
            {
                {columnName, errors[columnName]}
            };
            
        }

        protected static void TryGetLengthErrors( string propName, string field, Dictionary<string, IEnumerable<string>> errors,
            int maxLength, int minLength = 0 )
        {
            if ( string.IsNullOrWhiteSpace(field) ) return;

            var propErrors = new List<string>();

            if ( minLength > 0 && field.Length < minLength )
            {
                propErrors.Add( $" il campo {propName} deve avere una lunghezza minima di {minLength} caratteri" );
            }

            if ( field.Length > maxLength )
            {
                propErrors.Add( $" il campo {propName} deve avere una lunghezza massima di {maxLength} caratteri" );
            }

            if ( propErrors.Any() )
            {
                errors.Add( propName, propErrors );
            }
        }

        protected static bool TryAddNotNullError( string propName, object propValue, Dictionary<string, IEnumerable<string>> errors )
        {
            if ( propValue == null ) return false;
            if ( !string.IsNullOrWhiteSpace( propValue as string ) ) return false;

            var errorMsg = $"Il campo {propName} ritenuta deve essere valorizzato";
            errors.Add( propName, new List<string> { errorMsg } );
            return true;
        }

    }
}