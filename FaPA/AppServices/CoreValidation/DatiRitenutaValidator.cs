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
            const string importoritenuta = nameof(instnce.ImportoRitenuta);
            const string aliquotaritenuta = nameof(instnce.AliquotaRitenuta);
            const string ritenutaNonPuòEssereZero = "Importo ritenuta non può essere zero";
            var aliquotaNonPuòEssereZero = "AliquotaRitenuta ritenuta non può essere zero";
            if (instnce.ImportoRitenuta == 0)
            {
                
                if (!errors.ContainsKey(importoritenuta))
                    errors.Add(importoritenuta, new List<string> {ritenutaNonPuòEssereZero});
            }


            if ( instnce.AliquotaRitenuta == 0 )
            {

                if ( !errors.ContainsKey( aliquotaritenuta ) )
                    errors.Add( aliquotaritenuta, new List<string> { aliquotaNonPuòEssereZero });
            }

            
            return errors;
        }
    }
}