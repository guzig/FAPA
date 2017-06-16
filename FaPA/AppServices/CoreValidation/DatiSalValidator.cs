using System.Collections.Generic;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class DatiSalValidator : BaseCoreValidator
    {
        public override IDictionary<string, List<string>> GetValidationErrors( object instance )
        {
            var errors = new Dictionary<string, List<string>>();
            var instnce = instance as DatiSALType;

            if ( instnce == null ) return errors;


            if ( !string.IsNullOrWhiteSpace( instnce.RiferimentoFase ) )
            {
                int app;
                if ( !int.TryParse( instnce.RiferimentoFase, out app ) )
                {
                    errors.Add( nameof( instnce.RiferimentoFase ), new List<string>(1) {"Riferimento fase deve essere numerico di tre cifre"} );
                }
                return errors;
            }

            TryGetLengthErrors( nameof( instnce.RiferimentoFase ), instnce.RiferimentoFase, errors, 3, 0, false );

            return errors;
        }
    }
}