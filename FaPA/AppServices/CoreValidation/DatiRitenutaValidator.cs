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
            const string ritenutaNonPu�EssereZero = "Importo ritenuta non pu� essere zero";
            var aliquotaNonPu�EssereZero = "AliquotaRitenuta ritenuta non pu� essere zero";
            if (instnce.ImportoRitenuta == 0)
            {
                
                if (!errors.ContainsKey(importoritenuta))
                    errors.Add(importoritenuta, new List<string> {ritenutaNonPu�EssereZero});
            }


            if ( instnce.AliquotaRitenuta == 0 )
            {

                if ( !errors.ContainsKey( aliquotaritenuta ) )
                    errors.Add( aliquotaritenuta, new List<string> { aliquotaNonPu�EssereZero });
            }

            
            return errors;
        }
    }
}