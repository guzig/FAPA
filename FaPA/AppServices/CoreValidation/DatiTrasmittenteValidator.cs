using System.Collections.Generic;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class DatiTrasmittenteValidator : BaseCoreValidator
    {
        public override IDictionary<string, List<string>> GetValidationErrors( object instance )
        {
            var errors = new Dictionary<string, List<string>>();
            var instnce = instance as DatiTrasmissioneType;

            if (  instnce != null )
            {
                TryGetLengthErrors( nameof( instnce.CodiceDestinatario ), instnce.CodiceDestinatario, errors, 6, 7, false );
                TryGetLengthErrors( nameof( instnce.ProgressivoInvio ), instnce.ProgressivoInvio, errors, 5, 5, false);

                if ( instnce.IdTrasmittente != null )
                {
                    TryGetLengthErrors( nameof( instnce.IdTrasmittente.IdCodice ), instnce.IdTrasmittente.IdCodice, 
                        errors, 28, 1, false);

                    TryGetLengthErrors( nameof( instnce.IdTrasmittente.IdPaese ), instnce.IdTrasmittente.IdPaese, 
                        errors, 2, 2, false);
                }

                if ( instnce.ContattiTrasmittente != null )
                {
                    TryGetLengthErrors( nameof( instnce.ContattiTrasmittente.Email ), instnce.CodiceDestinatario, 
                        errors, 7, 256 );

                    TryGetLengthErrors( nameof( instnce.ContattiTrasmittente.Telefono ), instnce.CodiceDestinatario, 
                        errors, 5, 12 );
                }
            }

            return errors;
        }
    }
}