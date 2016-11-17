using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace FaPA.Infrastructure.Finder
{
    public class DoublePropCriterionValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var bindingGroup = (BindingGroup)value;

            var searchProperty = bindingGroup.Items[0] as INumsSearchProperty;

            if ( searchProperty == null )
                return new ValidationResult(false, "Validazione non riuscita");

            return Validate(searchProperty, bindingGroup, searchProperty.OperatorType);

        }

        private static ValidationResult Validate( INumsSearchProperty searchProperty, BindingGroup bindingGroup, NumOperatorEnums operatorType)
        {
            string errors = null;

            searchProperty.RootFinder.Validate();

            //validation raw value

            switch ( searchProperty.OperatorType )
            {
                case NumOperatorEnums.Equal:
                case NumOperatorEnums.NotEqual:
                case NumOperatorEnums.Less:
                case NumOperatorEnums.LessOrEqual:
                case NumOperatorEnums.Greater:
                case NumOperatorEnums.GreaterOrEqual:
                    object obj = bindingGroup.GetValue( searchProperty, "OperatorValue" );
                    double doubleObj;

                    if ( obj != null && !double.TryParse( obj.ToString(), out doubleObj ) )
                    {
                        searchProperty.RootFinder.IsValid = false;
                        return new ValidationResult( false,
                            string.Format( "il valore '{0}' non è un formato numerico valido", obj ) );
                    }

                    break;

                case NumOperatorEnums.Between:
                case NumOperatorEnums.NotBetween:
                    object minObj = bindingGroup.GetValue( searchProperty, "OperatorMinValue" );
                    object maxObj = bindingGroup.GetValue( searchProperty, "OperatorMaxValue" );
                    double doubleMinObj;
                    if ( minObj != null && !double.TryParse( minObj.ToString(), out doubleMinObj ) )
                    {
                        searchProperty.RootFinder.IsValid = false;
                        return new ValidationResult( false, "è richiesto un valore numerico minimo " );
                    }
                    double doubleMaxObj;
                    if ( maxObj == null || !double.TryParse( maxObj.ToString(), out doubleMaxObj ) )
                    {
                        searchProperty.RootFinder.IsValid = false;
                        return new ValidationResult( false, "è richiesto un valore numerico massimo maggiore di zero" );
                    }

                    break;
            }

            errors = ( from error in searchProperty.GetBrokenRules( "" ) select error ).FirstOrDefault();

            if ( string.IsNullOrWhiteSpace( errors ) )
                if ( searchProperty.RootFinder.IsValid )
                    return ValidationResult.ValidResult;
                else
                    searchProperty.RootFinder.IsValid = false;

            return String.IsNullOrWhiteSpace( errors ) ? ValidationResult.ValidResult : new ValidationResult( false, errors );
        }
    }
}