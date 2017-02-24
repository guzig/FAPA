using System;
using System.Collections.Generic;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{

    public class FatturaPrincipaleValidator : BaseCoreValidator
    {
        public override IDictionary<string, List<string>> GetValidationErrors( object instance )
        {
            var errors = new Dictionary<string, List<string>>();
            var instnce = instance as FatturaPrincipaleType;

            if ( instnce == null ) return errors;

            TryGetLengthErrors(nameof(instnce.NumeroFatturaPrincipale), instnce.NumeroFatturaPrincipale, errors, 20, 0, false);

            if ( instnce.DataFatturaPrincipale == DateTime.MinValue )
            {
                var errorMsg = "Il campo DataFatturaPrincipale deve essere valorizzato";
                errors.Add(nameof(instnce.DataFatturaPrincipale), new List<string> { errorMsg });
            }
            return errors;
        }
    }
}