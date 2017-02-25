using System;
using System.Collections.Generic;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class DatiGeneraliDocumentoValidator : BaseCoreValidator
    {

        public override IDictionary<string, List<string>> GetValidationErrors( object obj )
        {
            try
            {
                var errors = new Dictionary<string, List<string>>();

                var instance = ( DatiGeneraliDocumentoType ) obj;

                ValidateByFunc( nameof( instance.ImportoTotaleDocumento ), 
                    instance.ImportoTotaleDocumento, errors, f=>f>0 || f<0, "Importo documento deve essere valorizzato");

                ValidateByFunc( nameof( instance.Data ), instance.Data, errors, f => f == DateTime.MinValue, "Data documento deve essere valorizzato" );
                TryGetMinMaxValueErrors( nameof( instance.ImportoTotaleDocumento ), instance.ImportoTotaleDocumento, errors, ( decimal? ) 0.01 );
                TryGetLengthErrors( nameof( instance.Divisa ), instance.Divisa, errors, 5, 1, false );
                TryGetLengthErrors( nameof( instance.Numero ), instance.Numero, errors, 20, 1, false );

                if ( instance.Causale != null )
                {
                    foreach ( var causale in instance.Causale )
                    {
                        TryGetLengthErrors( nameof( instance.Causale ), causale, errors, 200 );
                    }
                }
                
                return errors;
            }
            catch ( Exception )
            {
                return null;
            }
        }
    }
}