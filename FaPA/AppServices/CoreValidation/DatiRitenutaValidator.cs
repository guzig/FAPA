using System.Collections.Generic;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class DatiRitenutaValidator : BaseCoreValidator
    {
        public override IDictionary<string, IEnumerable<string>> GetValidationErrors(object instance)
        {
            var instnce = instance as DatiRitenutaType;

            if ( instnce == null ) return Errors;

            if ( instnce.ImportoRitenuta == 0 )
            {
                const string importoritenuta = nameof( instnce.ImportoRitenuta );
                if ( !Errors.ContainsKey( importoritenuta ) )
                    Errors.Add( importoritenuta, new List<string> {"Importo ritenuta non può essere zero"});
            }

            if ( instnce.AliquotaRitenuta == 0 )
            {
                const string aliquotaritenuta = nameof( instnce.AliquotaRitenuta );
                if ( !Errors.ContainsKey( aliquotaritenuta ) )
                    Errors.Add( aliquotaritenuta, new List<string> { "AliquotaRitenuta ritenuta non può essere zero" });
            }

            return Errors;
        }
    }
}