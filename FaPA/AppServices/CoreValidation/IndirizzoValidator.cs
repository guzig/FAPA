using System.Collections.Generic;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class IndirizzoValidator : BaseCoreValidator
    {
        public override IDictionary<string, List<string>> GetValidationErrors( object instance )
        {
            var errors = new Dictionary<string, List<string>>();
            var instnce = instance as IndirizzoType;

            if ( instnce == null ) return errors;

            TryAddNotNullError( nameof( instnce.Nazione ), instnce.Nazione, errors );
            TryAddNotNullError( nameof( instnce.CAP ), instnce.CAP, errors );
            TryAddNotNullError( nameof( instnce.Comune ), instnce.Comune, errors );
            TryAddNotNullError( nameof( instnce.Indirizzo ), instnce.Indirizzo, errors );

            return errors;
        }
    }
}