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
            var propErrors = new List<string>();

            if ( minLength > 0 && !string.IsNullOrWhiteSpace( field ) && field.Length < minLength )
            {
                propErrors.Add( $" il campo {propName} deve avere una lunghezza minima di {minLength} caratteri" );
            }

            if ( !string.IsNullOrWhiteSpace( field ) && field.Length > maxLength )
            {
                propErrors.Add( $" il campo {propName} deve avere una lunghezza massima di {maxLength} caratteri" );
            }

            if ( propErrors.Any() )
            {
                errors.Add( propName, propErrors );
            }
        }

    }
}