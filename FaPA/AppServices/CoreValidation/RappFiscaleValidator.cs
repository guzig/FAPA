using System.Collections.Generic;
using System.Linq;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class RappFiscaleValidator : BaseCoreValidator
    {
        public override IDictionary<string, IEnumerable<string>> GetValidationErrors( object instance )
        {
            var errors = new Dictionary<string, IEnumerable<string>>();
            var instnce = instance as DatiAnagraficiRappresentanteType;

            if ( instnce == null ) return errors;

            if ( !TryAddNotNullError( nameof( instnce.Anagrafica ), instnce.Anagrafica, errors ) )
            {
                var vettore = instnce.Anagrafica.Validate();
                if ( !vettore.Success )
                {
                    errors.Add( nameof( instnce.Anagrafica ), vettore.Errors.Values.SelectMany( s => s ) );
                }
            }

            if ( !TryAddNotNullError( nameof( instnce.IdFiscaleIVA ), instnce.IdFiscaleIVA, errors ) )
            {
                var idfiscale = instnce.IdFiscaleIVA.Validate();
                if ( !idfiscale.Success )
                {
                    errors.Add( nameof( instnce.IdFiscaleIVA ), idfiscale.Errors.Values.SelectMany( s => s ) );
                }
            }

            TryGetLengthErrors( nameof( instnce.CodiceFiscale ), instnce.CodiceFiscale, errors, 11, 16 );

            return errors;
        }
    }
}