using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;


namespace FaPA.GUI.Feautures.Fattura
{
    public class UniqueFatturaValidationRule : ValidationRule
    {
        private static readonly string[] ItemLevelValidationProps = 
            { "NumeroFatturaDB", "DataFatturaDB", "AnagraficaCedenteDB" };

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var bindingGroup = value as BindingGroup;

            if (bindingGroup == null)
                return new ValidationResult(false, "UniqueFatturaValidationRule should only be used with a BindingGroup");

            if ( bindingGroup.Items != null && bindingGroup.Items.Count > 1)
            {
                var item = bindingGroup.Items[0];
                var viewModel = item as EditFatturaViewModel;
                var fattura = viewModel?.CurrentEntity;
                if ( fattura == null ) return ValidationResult.ValidResult;

                //fattura.Validate();

                if ( !fattura.DomainResult.Success && fattura.DomainResult.Errors != null &&
                     fattura.DomainResult.Errors.Any( i=> ItemLevelValidationProps.Contains( i.Key ) ) )
                    return new ValidationResult(false, "Numero fattura già registrato");
            }
            return ValidationResult.ValidResult;
        }
    }
}
