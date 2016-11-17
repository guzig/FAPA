using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace FaPA.Infrastructure.Finder
{
    public class StringPropCriterionValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var result = ValidationResult.ValidResult;

            var bindingGroup = (BindingGroup)value;

            if ( bindingGroup == null ) return new ValidationResult( false, "Validazione non riuscita" );
            
            var searchProperty = bindingGroup.Items[0] as ISearchProperty;

            if (searchProperty== null)
                return new ValidationResult(false, "Validazione non riuscita");
            
            searchProperty.RootFinder.Validate();

            if (searchProperty.RootFinder.IsValid)
                return result;
            
            var errors = (from error in searchProperty.GetBrokenRules("") select error).FirstOrDefault();

            return String.IsNullOrWhiteSpace(errors) ? result : new ValidationResult(false, errors) ;
        }
    }
}