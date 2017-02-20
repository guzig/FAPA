using System.Collections.Generic;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class ScontoMaggiorazioneValidator : BaseCoreValidator
    {
        public override IDictionary<string, List<string>> GetValidationErrors( object instance )
        {
            var errors = new Dictionary<string, List<string>>();
            var instnce = instance as ScontoMaggiorazioneType;

            if ( instnce == null ) return errors;

            TryGetLengthErrors( nameof( instnce.Importo ), instnce.Importo.ToString( "{0:0.00}" ), errors, 15, 4, false);
            TryGetLengthErrors( nameof( instnce.Percentuale ), instnce.Percentuale.ToString( "##.#0" ), errors, 6, 4, false);

            return errors;
        }
    }
}
