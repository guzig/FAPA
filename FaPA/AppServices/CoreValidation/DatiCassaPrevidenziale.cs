using System;
using System.Collections.Generic;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class DatiCassaPrevidenzialeValidator : BaseCoreValidator
    {

        public override IDictionary<string, List<string>> GetValidationErrors( object obj )
        {
            try
            {
                var errors = new Dictionary<string, List<string>>();

                var instance = ( DatiCassaPrevidenzialeType ) obj;

                TryGetMinMaxValueErrors( nameof( instance.AlCassa ), instance.AlCassa, errors,  ( decimal? ) 0.01 );
                TryGetMinMaxValueErrors( nameof( instance.ImportoContributoCassa ), instance.ImportoContributoCassa, errors, ( decimal? ) 0.01 );
                TryGetMinMaxValueErrors( nameof( instance.AliquotaIVA ), instance.AliquotaIVA, errors, ( decimal? ) 0.01 );

                TryGetLengthErrors( nameof( instance.RiferimentoAmministrazione ), instance.RiferimentoAmministrazione, errors, 20 );
                return errors;
            }
            catch ( Exception )
            {
                return null;
            }
        }
    }
}