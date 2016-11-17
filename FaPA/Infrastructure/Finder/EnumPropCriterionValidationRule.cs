using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace FaPA.Infrastructure.Finder
{
    public class EnumPropCriterionValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var bindingGroup = (BindingGroup)value;

            if (bindingGroup == null )
                return new ValidationResult(false, "Validazione non riuscita");

            var searchProperty = bindingGroup.Items[0] as EnumSearchProperty;

            if (searchProperty == null)
                return new ValidationResult(false, "Validazione non riuscita");

            string errors = null;

            searchProperty.RootFinder.Validate();

            errors = (from error in searchProperty.GetBrokenRules("") select error).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(errors))
                if (searchProperty.RootFinder.IsValid)
                    return ValidationResult.ValidResult;
                else
                    searchProperty.RootFinder.IsValid = false;

            return String.IsNullOrWhiteSpace(errors) ? ValidationResult.ValidResult : new ValidationResult(false, errors);
        }
    }
}