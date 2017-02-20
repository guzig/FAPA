using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{

    public class AltriDatiGestionaliValidator : BaseCoreValidator
    {
        public override IDictionary<string, List<string>> GetValidationErrors(object instance)
        {
            var errors = new Dictionary<string, List<string>>();
            var instnce = instance as AltriDatiGestionaliType;
            var propErrors = new List<string>();

            if ( instnce == null ) return errors;

            TryGetLengthErrors(nameof(instnce.TipoDato), instnce.TipoDato, errors, 20, 0, false );
            TryGetLengthErrors(nameof(instnce.RiferimentoTesto), instnce.TipoDato, errors, 60 );
            TryGetLengthErrors(nameof(instnce.RiferimentoNumero), instnce.
                RiferimentoNumero.ToString( CultureInfo.InvariantCulture ), errors, 4, 21);


            if ( propErrors.Any() )
            {
                errors.Add( nameof( instnce.TipoDato ), propErrors );
            }

            return errors;
        }
    }
}