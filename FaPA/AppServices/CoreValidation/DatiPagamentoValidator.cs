using System;
using System.Collections.Generic;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class DatiPagamentoValidator : BaseCoreValidator
    {

        public override IDictionary<string, IEnumerable<string>> GetValidationErrors(object instance)
        {
            try
            {
                var errors = new Dictionary<string, IEnumerable<string>>();

                DettaglioPagamentoType dettaglio;
                if ( instance is DatiPagamentoType )
                {
                    var instnce = ( DatiPagamentoType ) instance;
                    if ( instnce?.DettaglioPagamento?[0] == null ) return errors;
                    dettaglio = instnce?.DettaglioPagamento?[0];
                }
                else if ( instance is DettaglioPagamentoType )
                {
                    dettaglio = ( DettaglioPagamentoType ) instance;
                }
                else
                {
                    return errors;
                }
                
                TryGetLengthErrors( nameof( dettaglio.Beneficiario), dettaglio.Beneficiario, errors, 200 );
                TryGetLengthErrors( nameof( dettaglio.GiorniTerminiPagamento ), dettaglio.GiorniTerminiPagamento, errors, 3 );
                TryGetLengthErrors( nameof( dettaglio.CodUfficioPostale ), dettaglio.CodUfficioPostale, errors, 20 );
                TryGetLengthErrors( nameof( dettaglio.CognomeQuietanzante ), dettaglio.CognomeQuietanzante, errors, 60 );
                TryGetLengthErrors( nameof( dettaglio.NomeQuietanzante ), dettaglio.NomeQuietanzante, errors, 60 );
                TryGetLengthErrors( nameof( dettaglio.CfQuietanzante ), dettaglio.CfQuietanzante, errors, 16 );
                TryGetLengthErrors( nameof( dettaglio.TitoloQuietanzante ), dettaglio.TitoloQuietanzante, errors, 10, 2, true );
                TryGetLengthErrors( nameof( dettaglio.IstitutoFinanziario ), dettaglio.IstitutoFinanziario, errors, 80 );
                TryGetLengthErrors( nameof( dettaglio.Iban ), dettaglio.Iban, errors, 5 );
                TryGetLengthErrors( nameof( dettaglio.Abi ), dettaglio.Abi, errors, 5 );
                TryGetLengthErrors( nameof( dettaglio.Cab ), dettaglio.Cab, errors, 5 );
                TryGetLengthErrors( nameof( dettaglio.Bic ), dettaglio.Bic, errors, 5 );
                TryGetLengthErrors( nameof( dettaglio.CodicePagamento ), dettaglio.CodicePagamento, errors, 60 );

                return errors;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}