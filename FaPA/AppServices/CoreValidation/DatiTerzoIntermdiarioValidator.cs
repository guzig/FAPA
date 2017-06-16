using System.Collections.Generic;
using System.Linq;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class DatiTerzoIntermdiarioValidator : BaseCoreValidator
    {
        public override IDictionary<string, List<string>> GetValidationErrors( object instance )
        {
            var errors = new Dictionary<string, List<string>>();

            var ti = instance as DatiAnagraficiTerzoIntermediarioType;
            if ( ti != null )
            {
                TryGetLengthErrors( nameof( ti.CodiceFiscale ), ti.CodiceFiscale, errors, 16, 11 );
                return errors;
            }

            var instnce = instance as TerzoIntermediarioSoggettoEmittenteType;

            if ( instnce == null ) return errors;

            if ( instnce.DatiAnagrafici?.Anagrafica != null )
            {
                var result = instnce.DatiAnagrafici.Anagrafica.Validate();
                if ( !result.Success )
                {
                    errors.Add( nameof( instnce.DatiAnagrafici.Anagrafica ), result.Errors.Values.SelectMany( s => s ).ToList() );
                }
            }

            //if ( !string.IsNullOrWhiteSpace(instnce.DatiAnagrafici.IdFiscaleIVA.IdCodice) ||
            //    !string.IsNullOrWhiteSpace( instnce.DatiAnagrafici.IdFiscaleIVA.IdPaese )  )
            //{
            //    var idfiscale = instnce.DatiAnagrafici.IdFiscaleIVA.Validate();
            //    if ( !idfiscale.Success )
            //    {
            //        errors.Add( nameof( instnce.DatiAnagrafici.IdFiscaleIVA ), idfiscale.Errors.Values.SelectMany( s => s ).ToList() );
            //    }

            //    foreach ( var error in idfiscale.Errors )
            //    {
            //        errors.Add( "DatiAnagrafici.IdFiscaleIVA.IdCodice", error.Value );
            //    }
            //}



            return errors;
        }
    }
}