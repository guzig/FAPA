using System.Collections.Generic;
using System.Linq;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class DatiCorrelatiValidator : BaseCoreValidator
    {
        public override IDictionary<string, IEnumerable<string>> GetValidationErrors(object instance)
        {
            var errors = new Dictionary<string, IEnumerable<string>>();
            var instnce = instance as DatiDocumentiCorrelatiType;
            var propErrors = new List<string>();

            if ( instnce == null ) return errors;

            const string iddocumento = nameof(instnce.IdDocumento);

            if ( string.IsNullOrWhiteSpace(instnce.IdDocumento))
                propErrors.Add( "IdDocumento deve essere valorizzato" );
            else if (instnce.IdDocumento.Length > 20)
                propErrors.Add("IdDocumento deve essere lungo max 20 caratteri");

            if ( propErrors.Any() )
                errors.Add( iddocumento, propErrors );

            return errors;
        }
    }
}