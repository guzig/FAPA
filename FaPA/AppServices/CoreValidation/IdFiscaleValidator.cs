using System.Collections.Generic;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class IdFiscaleValidator : BaseCoreValidator
    {
        public override IDictionary<string, List<string>> GetValidationErrors( object instance )
        {
            var errors = new Dictionary<string, List<string>>();
            var instnce = instance as IdFiscaleType;

            if ( instnce == null ) return errors;

            TryGetLengthErrors(nameof(instnce.IdCodice), instnce.IdCodice, errors, 28, 0, false); 
            TryGetLengthErrors( nameof(instnce.IdPaese), instnce.IdPaese, errors, 2, 0, false);

            return errors;
        }
    }
}