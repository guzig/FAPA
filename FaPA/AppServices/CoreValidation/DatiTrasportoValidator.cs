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

            TryGetLengthErrors(nameof(instnce.MezzoTrasporto), instnce.MezzoTrasporto, errors, 80);
            TryGetLengthErrors(nameof(instnce.CausaleTrasporto), instnce.CausaleTrasporto, errors, 100);
            TryGetLengthErrors(nameof(instnce.NumeroColli), instnce.NumeroColli, errors, 4);
            TryGetLengthErrors(nameof(instnce.Descrizione), instnce.NumeroColli, errors, 100);
            TryGetLengthErrors(nameof(instnce.UnitaMisuraPeso), instnce.NumeroColli, errors, 10);
            TryGetLengthErrors(nameof(instnce.PesoLordo), instnce.NumeroColli, errors, 7,4);
            TryGetLengthErrors(nameof(instnce.PesoNetto), instnce.NumeroColli, errors, 7, 4);
            TryGetLengthErrors(nameof(instnce.TipoResa), instnce.TipoResa, errors, 3);

            return errors;
        }
    }
}