using System;
using System.Collections.Generic;
using System.Linq;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public class DatiDdtValidator : BaseCoreValidator
    {
        public override IDictionary<string, List<string>> GetValidationErrors( object instance )
        {
            var errors = new Dictionary<string, List<string>>();
            var instnce = instance as DatiDDTType;

            if ( instnce == null ) return errors;

            TryGetLengthErrors( nameof( instnce.NumeroDDT ), instnce.NumeroDDT, errors, 20, 0, false );

            if ( instnce.DataDDT == DateTime.MinValue )
            {
                errors.Add( nameof( instnce.DataDDT ), new List<string>( 1 ) { "Data deve essere valorizzata" } );
            }

            if ( instnce.RiferimentoNumeroLinea != null && 
                 instnce.RiferimentoNumeroLinea.Any(s=>!string.IsNullOrWhiteSpace( s ) ) )
            {
                foreach ( var item in instnce.RiferimentoNumeroLinea )
                {
                    TryGetLengthErrors( nameof( instnce.RiferimentoNumeroLinea ), item, errors, 4, 0, false );
                }
            }
            return errors;
        }
    }
}