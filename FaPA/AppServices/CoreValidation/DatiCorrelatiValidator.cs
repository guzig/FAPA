using System.Collections.Generic;
using System.Linq;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class DatiCorrelatiValidator : BaseCoreValidator
    {
        public override IDictionary<string, List<string>> GetValidationErrors(object instance)
        {
            var errors = new Dictionary<string, List<string>>();
            var instnce = instance as DatiDocumentiCorrelatiType;
            var propErrors = new List<string>();

            if ( instnce == null ) return errors;

            const string iddocumento = nameof(instnce.IdDocumento);

            TryGetLengthErrors(nameof(instnce.IdDocumento), instnce.IdDocumento, errors, 28, 0, false);

            if ( propErrors.Any() )
                errors.Add( iddocumento, propErrors );

            return errors;
        }
    }
}