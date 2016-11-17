using System;
using System.Collections.Generic;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class DettaglioPagamentoValidator : BaseCoreValidator
    {

        public override IDictionary<string, IEnumerable<string>> GetValidationErrors(object instance)
        {
            try
            {
                var errors = new Dictionary<string, IEnumerable<string>>();
                var instnce = instance as DatiPagamentoType;

                if ( instnce?.DettaglioPagamento?[0] == null) return errors;

                var importopagamento = "ImportoPagamento";
                //if ( !errors.ContainsKey(importopagamento))
                //{
                //    errors.Add(importopagamento,
                //        new List<string> { "Importo pagamento non può essere minore di zero" });
                //}

                var dettPagamento = instnce.DettaglioPagamento[0];
                if ( dettPagamento.ImportoPagamento > 10 )
                {
                    if (!errors.ContainsKey(importopagamento))
                    {
                        errors.Add(importopagamento, 
                            new List<string> { "Importo pagamento non può essere minore di zero" });
                    }
                }

                return errors;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}