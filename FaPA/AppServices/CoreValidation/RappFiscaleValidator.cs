using System.Collections.Generic;
using System.Linq;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class RappFiscaleValidator : BaseCoreValidator
    {
        public override IDictionary<string, List<string>> GetValidationErrors( object instance )
        {
            var errors = new Dictionary<string, List<string>>();
            var instnce = instance as DatiAnagraficiRappresentanteType;

            if ( instnce == null ) return errors;

            if ( instnce.Anagrafica != null )
            {
                var vettore = instnce.Anagrafica.Validate();
                if ( !vettore.Success )
                {
                    errors.Add( nameof( instnce.Anagrafica ), vettore.Errors.Values.SelectMany( s => s ).ToList() );
                }
            }

            if ( instnce.IdFiscaleIVA != null )
            {
                var idfiscale = instnce.IdFiscaleIVA.Validate();
                if ( !idfiscale.Success )
                {
                    errors.Add( nameof( instnce.IdFiscaleIVA ), idfiscale.Errors.Values.SelectMany( s => s ).ToList() );
                }
            }

            TryGetLengthErrors( nameof( instnce.CodiceFiscale ), instnce.CodiceFiscale, errors, 11, 16, false );

            return errors;
        }
    }
}