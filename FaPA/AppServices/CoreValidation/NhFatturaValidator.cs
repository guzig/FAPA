using System.Collections.Generic;
using System.Linq;
using FaPA.DomainServices.Utils;

namespace FaPA.AppServices.CoreValidation
{
    public class NhFatturaValidator : NhInstanceValidator
    {
        protected override IEnumerable<string> ItemLevelValidationGroup => new[] { "NumeroFatturaDB", "DataFatturaDB", "AnagraficaCedenteDB" };

        public override IDictionary<string, IEnumerable<string>> GetValidationErrors(string columnName, object instance)
        {
            if ( Validator == null ) return new Dictionary<string, IEnumerable<string>>();

            if ( ItemLevelValidationGroup.Contains(columnName))
                return GetValidationErrors(instance);

            var errors =  Validator.ValidatePropertyValue(instance, columnName).
                DistinctBy(d => d.Message).Select(d => d.Message).ToList();

            return errors.Any() ? new Dictionary<string, IEnumerable<string>> { { columnName, errors } } : 
                                  new Dictionary<string, IEnumerable<string>>();
        }
    }
}