using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace FaPA.Infrastructure.Finder
{
    public class Int64PropCriterionValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var bindingGroup = (BindingGroup)value;
          
            var searchProperty = bindingGroup.Items[0] as Int64SearchProperty;

            if (searchProperty == null)
                return new ValidationResult(false, "Validazione non riuscita");

            string errors = null;

            searchProperty.RootFinder.Validate();

            //validation raw value
            switch (searchProperty.OperatorType)
            {
                case NumOperatorEnums.Equal:
                case NumOperatorEnums.NotEqual:
                case NumOperatorEnums.Less:
                case NumOperatorEnums.LessOrEqual:
                case NumOperatorEnums.Greater:
                case NumOperatorEnums.GreaterOrEqual:
                    object obj;
                    if ( !bindingGroup.TryGetValue( searchProperty, "OperatorValue", out obj ) ) 
                        return ValidationResult.ValidResult;

                    Int64 longObj;

                    if (obj != null && !Int64.TryParse(obj.ToString(), out longObj))
                    {
                        searchProperty.RootFinder.IsValid = false;
                        const string msgFormatError = "il valore '{0}' non è un formato numerico valido";
                        return new ValidationResult(false, string.Format(msgFormatError, obj));
                    }

                    break;

                case NumOperatorEnums.Between:
                case NumOperatorEnums.NotBetween:
                    object minObj;
                    const string minErrorMsg = "è richiesto un valore numerico minimo ";
                    if (!bindingGroup.TryGetValue(searchProperty, "OperatorMinValue", out minObj))
                        return new ValidationResult( true, "" );
                        //return new ValidationResult(false, minErrorMsg);

                    object maxObj;
                    const string maxErrorMsg = "è richiesto un valore numerico massimo maggiore di zero ";
                    if ( !bindingGroup.TryGetValue(searchProperty, "OperatorMaxValue", out  maxObj) )
                        return new ValidationResult( true, "" );
                        //return new ValidationResult(false, maxErrorMsg);

                    Int64 longMinObj;
                    if (minObj != null && !Int64.TryParse(minObj.ToString(), out longMinObj))
                    {
                        searchProperty.RootFinder.IsValid = false;
                        return new ValidationResult(false, minErrorMsg);
                    }
                    Int64 longMaxObj;
                    if (maxObj == null || !Int64.TryParse(maxObj.ToString(), out longMaxObj))
                    {
                        searchProperty.RootFinder.IsValid = false;
                        return new ValidationResult(false, maxErrorMsg);
                    }
                    break;
                //case NumOperatorEnums.NoneOf:
                //case NumOperatorEnums.OneOf:
                //{
                //    if ( !bindingGroup.TryGetValue( searchProperty, "OperatorValues", out obj ) )
                //        return ValidationResult.ValidResult;
                //}
                //    break;
            }

            errors = (from error in searchProperty.GetBrokenRules("") select error).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(errors) &&  searchProperty.RootFinder.IsValid )
                return ValidationResult.ValidResult;
            
            searchProperty.RootFinder.IsValid = false;

            return String.IsNullOrWhiteSpace(errors) ? ValidationResult.ValidResult : new ValidationResult(false, errors);
        }
    }
}