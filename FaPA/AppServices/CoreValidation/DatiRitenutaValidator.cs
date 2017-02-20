using System.Collections.Generic;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class DatiRitenutaValidator : BaseCoreValidator
    {
        public override IDictionary<string, List<string>> GetValidationErrors(object instance)
        {
            var errors = new Dictionary<string, List<string>>();
            var instnce = instance as DatiRitenutaType;

            if ( instnce == null ) return errors;

            TryGetMinMaxValueErrors(nameof(instnce.ImportoRitenuta), instnce.ImportoRitenuta, errors, 1);
            TryGetMinMaxValueErrors(nameof(instnce.AliquotaRitenuta), instnce.AliquotaRitenuta, errors, 1);
            
            return errors;
        }
    }
}