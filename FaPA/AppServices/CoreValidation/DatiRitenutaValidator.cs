using System.Collections.Generic;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class DatiRitenutaValidator : BaseCoreValidator
    {
        public override IDictionary<string, IEnumerable<string>> GetValidationErrors(object instance)
        {
            var errors = new Dictionary<string, IEnumerable<string>>();
            var instnce = instance as DatiRitenutaType;

            if ( instnce == null ) return errors;

            if ( instnce.ImportoRitenuta == 0 )
            {
                errors.Add(nameof(instnce.ImportoRitenuta), new List<string> {"Importo ritenuta non può essere zero"});
            }

            if ( instnce.AliquotaRitenuta == 0 )
            {
                errors.Add(nameof(instnce.AliquotaRitenuta), new List<string> { "AliquotaRitenuta ritenuta non può essere zero" });
            }

            return errors;
        }
    }
}