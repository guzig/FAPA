using System.Collections.Generic;
using System.Linq;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class DettaglioLineeValidator : BaseCoreValidator
    {
        public override IDictionary<string, IEnumerable<string>> GetValidationErrors(object instance)
        {
            var errors = new Dictionary<string, IEnumerable<string>>();
            var instnce = instance as DettaglioLineeType;
            if (instnce == null) return errors;

            TryGetLengthErrors( nameof(instnce.NumeroLinea) , instnce.NumeroLinea, errors, 4, 1 );
            TryGetLengthErrors( nameof( instnce.Descrizione ), instnce.Descrizione, errors, 1000, 1 );
            TryGetLengthErrors( nameof( instnce.PrezzoUnitario ), instnce.PrezzoUnitario.ToString( "{0:###0.00}" ), errors, 21, 4 );
            TryGetLengthErrors( nameof( instnce.PrezzoTotale ), instnce.PrezzoTotale.ToString( "{0:###0.00}" ), errors, 21, 4 );
            TryGetLengthErrors( nameof( instnce.AliquotaIVA ), instnce.AliquotaIVA.ToString( "{0:###0.00}" ), errors, 21, 4 );
            TryGetMinMaxValueErrors( nameof( instnce.AliquotaIVA ), instnce.AliquotaIVA, errors, 1 );

            TryGetLengthErrors( nameof( instnce.Quantita ), instnce.Quantita.ToString( "{0:###0.00}" ), errors, 21 );
            TryGetLengthErrors( nameof( instnce.UnitaMisura ), instnce.UnitaMisura, errors, 10 );
            TryGetLengthErrors( nameof( instnce.RiferimentoAmministrazione ), instnce.RiferimentoAmministrazione, errors, 20 );

            const string propName = "AltriDatiGestionali";

            ValidateChild<AltriDatiGestionaliType>(instance, propName, errors  );
            
            return errors;
        }

    }
}