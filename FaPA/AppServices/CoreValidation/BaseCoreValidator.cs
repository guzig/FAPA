using System.Collections.Generic;
using System.Linq;

namespace FaPA.AppServices.CoreValidation
{
    public abstract class BaseCoreValidator : ICoreValidator
    {
      
        public abstract IDictionary<string, List<string>> GetValidationErrors(object instance);

        public virtual IDictionary<string, List<string>> GetValidationErrors(string columnName, object instance)
        {
            var errors = GetValidationErrors(instance);

            return new Dictionary<string, List<string>>
            {
                {columnName, errors[columnName]}
            };
            
        }

        protected static void TryGetLengthErrors( string propName, string field, Dictionary<string, List<string>> errors,
            int maxLength, int minLength=0, bool isNullAllowed=true )
        {
            var propErrors = new List<string>();

            if (string.IsNullOrWhiteSpace(field) && isNullAllowed) return;

            if (string.IsNullOrWhiteSpace(field) && !isNullAllowed)
            {
                propErrors.Add($" il campo {propName} è obbligatorio");
            }

            if ( minLength > 0 && ( string.IsNullOrWhiteSpace( field ) || field.Length < minLength ) )
            {
                propErrors.Add( $" il campo {propName} deve avere una lunghezza minima di {minLength} caratteri" );
            }

            if ( field != null && field.Length > maxLength )
            {
                propErrors.Add( $" il campo {propName} deve avere una lunghezza massima di {maxLength} caratteri" );
            }

            if ( propErrors.Any() )
            {
                errors.Add( propName, propErrors );
            }
        }

        protected static void TryGetMinMaxValueErrors( string propName, decimal value, Dictionary<string, List<string>> errors,
            decimal? minLength, decimal?  maxLength= null )
        {
            var propErrors = new List<string>();

            if ( minLength != null && value < minLength  )
            {
                propErrors.Add( $" il campo {propName} non deve essere minore di {minLength}" );
            }

            if ( maxLength != null && value > minLength )
            {
                propErrors.Add( $" il campo {propName} deve essere minore di {maxLength}" );
            }
            
            if ( propErrors.Any() )
            {
                errors.Add( propName, propErrors );
            }
        }

        protected static bool TryAddNotNullError( string propName, string propValue, Dictionary<string, List<string>> errors )
        {
            if ( !string.IsNullOrWhiteSpace( propValue ) ) return false;

            var errorMsg = $"Il campo {propName} ritenuta deve essere valorizzato";
            errors.Add( propName, new List<string> { errorMsg } );
            return true;
        }

        protected static void ValidateChild<T>(object instance, string propName, 
            Dictionary<string, List<string>> errors ) where T : class
        {
            var childs = ObjectExplorer.FindAllInstancesDeep<T>( instance );
            if ( childs == null || !childs.Any() ) return;
            
            foreach ( var child in childs )
            {
                var childErrors = CoreValidatorService.GetValidationErrors( child );
                if ( childErrors == null ) continue;
                foreach ( var erro in childErrors )
                {
                    if ( errors.ContainsKey( propName ) )
                    {
                        var temp = errors[propName].ToList();
                        temp.AddRange( erro.Value );
                        errors[propName] = temp;
                    }
                    else
                        errors.Add( propName, erro.Value );
                }
            }
        }

    }
}