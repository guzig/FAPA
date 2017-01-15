using System.Collections.Generic;
using FaPA.Core;
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

            if ( !string.IsNullOrWhiteSpace( instnce.Denominazione ) ) return errors;

            if ( string.IsNullOrWhiteSpace( instnce.Denominazione + instnce.Cognome ) )
            {
                TryAddNotNullError( nameof( instnce.Denominazione ), instnce.Denominazione, errors );
            }

            TryAddNotNullError( nameof( instnce.Cognome ), instnce.Cognome, errors );
            TryAddNotNullError( nameof( instnce.Nome ), instnce.Nome, errors );

            return errors;
        }
    }
}