using System.Collections.Generic;
using System.Linq;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class DatiTrasportoValidator : BaseCoreValidator
    {
        public override IDictionary<string, IEnumerable<string>> GetValidationErrors( object instance )
        {
            var errors = new Dictionary<string, IEnumerable<string>>();
            var instnce = instance as DatiTrasportoType;

            if ( instnce == null ) return errors;

            if ( instnce.DatiAnagraficiVettore  != null )
            {
                var vettore = instnce.DatiAnagraficiVettore.Validate();
                if ( !vettore.Success )
                {
                    errors.Add( "DatiAnagraficiVettore", vettore.Errors.Values.SelectMany( s => s ) );
                }
            }

            if ( instnce.IndirizzoResa != null )
            {
                var indirizzo = instnce.IndirizzoResa.Validate();
                if ( !indirizzo.Success )
                {
                    errors.Add( "IndirizzoResa", indirizzo.Errors.Values.SelectMany( s => s ) );
                }
            }

            return errors;
        }
    }
}