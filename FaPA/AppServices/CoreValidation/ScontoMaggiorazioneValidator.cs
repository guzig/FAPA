using System.Collections.Generic;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class ScontoMaggiorazioneValidator : BaseCoreValidator
    {
        public override IDictionary<string, IEnumerable<string>> GetValidationErrors( object instance )
        {
            var errors = new Dictionary<string, IEnumerable<string>>();
            var instnce = instance as ScontoMaggiorazioneType;

            if ( instnce == null ) return errors;

            TryGetLengthErrors( nameof( instnce.Importo ), instnce.Importo.ToString( "{0:0.00}" ), errors, 15, 4 );
            TryGetLengthErrors( nameof( instnce.Percentuale ), instnce.Percentuale.ToString( "##.#0" ), errors, 6, 4 );

            return errors;
        }
    }
}
