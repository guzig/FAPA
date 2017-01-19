using System.Collections.Generic;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class AnagraficaValidator : BaseCoreValidator
    {
        public override IDictionary<string, IEnumerable<string>> GetValidationErrors( object instance )
        {
            var errors = new Dictionary<string, IEnumerable<string>>();
            var instnce = instance as AnagraficaType;

            if ( instnce == null ) return errors;

            if ( !TryAddNotNullError(nameof(instnce.Denominazione), instnce.Denominazione, errors))
            {
                TryGetLengthErrors(nameof(instnce.Denominazione), instnce.Denominazione, errors, 80);
                TryGetLengthErrors(nameof(instnce.Nome), instnce.Nome, errors, 60);
                TryGetLengthErrors(nameof(instnce.Cognome), instnce.Cognome, errors, 60);
                return errors;
            }

            if (TryAddNotNullError(nameof(instnce.Nome), instnce.Nome, errors))
            {
                TryGetLengthErrors(nameof(instnce.Nome), instnce.Nome, errors, 60);
            }

            if (TryAddNotNullError(nameof(instnce.Cognome), instnce.Cognome, errors ) )
            {
                TryGetLengthErrors(nameof(instnce.Cognome), instnce.Cognome, errors, 60 );
            }

            return errors;
        }
    }
}