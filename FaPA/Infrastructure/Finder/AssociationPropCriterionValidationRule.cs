using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace FaPA.Infrastructure.Finder
{
    public class AssociationPropCriterionValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            var bindingGroup = (BindingGroup)value;

            var searchProperty = bindingGroup.Items[0] as ISearchProperty;

            //searchProperty.Validate();
            searchProperty.RootFinder.Validate();

            if (searchProperty.RootFinder.IsValid)
                return ValidationResult.ValidResult;

            var errors = (from error in searchProperty.GetBrokenRules("") select error).FirstOrDefault();

            return string.IsNullOrWhiteSpace(errors) ? ValidationResult.ValidResult : new ValidationResult(false, errors);


        }
    }
}
