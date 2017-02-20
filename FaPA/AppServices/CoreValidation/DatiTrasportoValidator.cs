using System.Collections.Generic;
using System.Linq;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class DatiTrasportoValidator : BaseCoreValidator
    {
        public override IDictionary<string, List<string>> GetValidationErrors( object instance )
        {
            var errors = new Dictionary<string, List<string>>();
            var instnce = instance as DatiTrasportoType;

            if ( instnce == null ) return errors;

            if ( instnce.DatiAnagraficiVettore  != null )
            {
                var vettore = instnce.DatiAnagraficiVettore.Validate();
                if ( !vettore.Success )
                {
                    errors.Add( "DatiAnagraficiVettore", vettore.Errors.Values.SelectMany( s => s ).ToList() );
                }
            }

            if ( instnce.IndirizzoResa != null )
            {
                var indirizzo = instnce.IndirizzoResa.Validate();
                if ( !indirizzo.Success )
                {
                    errors.Add( "IndirizzoResa", indirizzo.Errors.Values.SelectMany( s => s ).ToList() );
                }
            }

            TryGetLengthErrors(nameof(instnce.MezzoTrasporto), instnce.MezzoTrasporto, errors, 80);
            TryGetLengthErrors(nameof(instnce.CausaleTrasporto), instnce.CausaleTrasporto, errors, 100);
            TryGetLengthErrors(nameof(instnce.NumeroColli), instnce.NumeroColli, errors, 4);
            TryGetLengthErrors(nameof(instnce.Descrizione), instnce.Descrizione, errors, 100);
            TryGetLengthErrors(nameof(instnce.UnitaMisuraPeso), instnce.NumeroColli, errors, 10);
            //TryGetLengthErrors(nameof(instnce.PesoLordo), instnce.PesoLordo, errors, 7,4, true);
            //TryGetLengthErrors(nameof(instnce.PesoNetto), instnce.NumeroColli, errors, 7, 4);
            TryGetLengthErrors(nameof(instnce.TipoResa), instnce.TipoResa, errors, 3);

            return errors;
        }
    }
}