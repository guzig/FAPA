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

            var childs = ObjectExplorer.FindAllInstances<AltriDatiGestionaliType>(instance);
            if (childs == null || !childs.Any()) return errors;
            const string propName = "AltriDatiGestionali";

            foreach (var child in childs)
            {
                var childErrors = CoreValidatorService.GetValidationErrors(child);
                if (childErrors == null) continue;
                foreach (var erro in childErrors)
                {
                    if ( errors.ContainsKey( propName ) )
                    {
                        var temp = errors[propName].ToList();
                        temp.AddRange(erro.Value);
                        errors[propName] = temp;
                    }
                    else
                        errors.Add( propName, erro.Value);
                }
            }

            //if (string.IsNullOrWhiteSpace(instnce.IdDocumento))
            //    propErrors.Add("IdDocumento deve essere valorizzato");
            //else if (instnce.IdDocumento.Length > 20)
            //    propErrors.Add("IdDocumento deve essere lungo max 20 caratteri");

            //errors.Add(nameof(instnce.IdDocumento), propErrors);

            return errors;
        }
    }
}