using System.Collections.Generic;
using System.Linq;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class DatiGeneraliDocumentoValidator : BaseCoreValidator
    {
        public override IDictionary<string, IEnumerable<string>> GetValidationErrors(object instance)
        {
            var instnce = instance as DatiGeneraliDocumentoType;

            var validationErrors = new DatiRitenutaValidator().GetValidationErrors(instnce.DatiRitenuta);

            if ( Errors.Any() )
                Errors.Add("DatiRitenuta", validationErrors.SelectMany( s => s.Value ) ); 

            return Errors;
        }
    }
}