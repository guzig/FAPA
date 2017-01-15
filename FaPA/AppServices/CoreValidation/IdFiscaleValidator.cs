using System.Collections.Generic;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class IdFiscaleValidator : BaseCoreValidator
    {
        public override IDictionary<string, IEnumerable<string>> GetValidationErrors( object instance )
        {
            var errors = new Dictionary<string, IEnumerable<string>>();
            var instnce = instance as IdFiscaleType;

            if ( instnce == null ) return errors;

            TryAddNotNullError( nameof( instnce.IdCodice ), instnce.IdCodice, errors );
            TryAddNotNullError( nameof( instnce.IdPaese ), instnce.IdPaese, errors );

            return errors;
        }
    }
}