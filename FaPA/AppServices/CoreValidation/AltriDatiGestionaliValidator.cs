using System.Collections.Generic;
using System.Linq;
using FaPA.Core.FaPa;
using Xceed.Wpf.Toolkit;

namespace FaPA.AppServices.CoreValidation
{

    public class AltriDatiGestionaliValidator : BaseCoreValidator
    {
        public override IDictionary<string, IEnumerable<string>> GetValidationErrors(object instance)
        {
            var errors = new Dictionary<string, IEnumerable<string>>();
            var instnce = instance as AltriDatiGestionaliType;
            var propErrors = new List<string>();

            if ( instnce == null ) return errors;

            if ( string.IsNullOrWhiteSpace( instnce.TipoDato ) )
                propErrors.Add( "TipoDato deve essere valorizzato" );
            else if ( instnce.TipoDato.Length > 20 )
                propErrors.Add( "TipoDato deve essere lungo max 20 caratteri" );

            //opt
            //rif. testo 60
            //RiferimentoNumero: 4-21

            if ( propErrors.Any() )
            {
                errors.Add( nameof( instnce.TipoDato ), propErrors );
            }

            return errors;
        }
    }
}