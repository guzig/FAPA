using System.Collections.Generic;
using System.Linq;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class DatiAnagraficiVettoreValidator : BaseCoreValidator
    {
        public override IDictionary<string, IEnumerable<string>> GetValidationErrors( object instance )
        {
            var errors = new Dictionary<string, IEnumerable<string>>();
            var instnce = instance as DatiAnagraficiVettoreType;

            if ( instnce == null ) return errors;

            if ( instnce.IdFiscaleIVA != null )
            {
                var idFiscaleResult = instnce.IdFiscaleIVA.Validate();
                if ( !idFiscaleResult.Success )
                    errors.Add( "IdFiscaleIVA", idFiscaleResult.Errors.Values.SelectMany( s => s ) );
            }
            else
            {
                errors.Add( "IdFiscaleIVA", new List<string>(1) {"IdFiscale deve essere valorizzato"} );
            }

            if (instnce.Anagrafica != null)
            {
                var anagResult = instnce.Anagrafica.Validate();
                if (!anagResult.Success)
                    errors.Add("Anagrafica", anagResult.Errors.Values.SelectMany(s => s));
            }
            else
            {
                errors.Add("Anagrafica", new List<string>(1) { "Anagrafica deve essere valorizzato" });
            }

            return errors;
        }
    }
}