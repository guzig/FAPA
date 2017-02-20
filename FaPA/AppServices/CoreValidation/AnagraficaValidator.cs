using System.Collections.Generic;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class AnagraficaValidator : BaseCoreValidator
    {
        public override IDictionary<string, List<string>> GetValidationErrors( object instance )
        {
            var errors = new Dictionary<string, List<string>>();
            var instnce = instance as AnagraficaType;

            if ( instnce == null ) return errors;

            if ( !TryAddNotNullError(nameof(instnce.Denominazione), instnce.Denominazione, errors))
            {
                TryGetLengthErrors(nameof(instnce.Denominazione), instnce.Denominazione, errors, 80);
                TryGetLengthErrors(nameof(instnce.Nome), instnce.Nome, errors, 60);
                TryGetLengthErrors(nameof(instnce.Cognome), instnce.Cognome, errors, 60);
                return errors;
            }

            TryGetLengthErrors(nameof(instnce.Nome), instnce.Nome, errors, 60, 0, false);
            TryGetLengthErrors(nameof(instnce.Cognome), instnce.Cognome, errors, 60, 0, false );
 
            return errors;
        }
    }
}