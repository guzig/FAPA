using System;
using System.Collections.Generic;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class DatiCassaBolloValidator : BaseCoreValidator
    {

        public override IDictionary<string, List<string>> GetValidationErrors( object obj )
        {
            try
            {
                var errors = new Dictionary<string, List<string>>();

                var instance = ( DatiBolloType ) obj;

                if ( instance.BolloVirtuale == BolloVirtualeType.SI )
                    TryGetMinMaxValueErrors( nameof( instance.ImportoBollo ), instance.ImportoBollo, errors, ( decimal? ) 0.01 );

                return errors;
            }
            catch ( Exception )
            {
                return null;
            }
        }
    }
}