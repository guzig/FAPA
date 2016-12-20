using System.Collections.Generic;
using FaPA.Core.FaPa;

namespace FaPA.AppServices.CoreValidation
{
    public abstract class BaseCoreValidator : ICoreValidator
    {
      
        public abstract IDictionary<string, IEnumerable<string>> GetValidationErrors(object instance);

        public virtual IDictionary<string, IEnumerable<string>> GetValidationErrors(string columnName, object instance)
        {
            var errors = GetValidationErrors(instance);

            return new Dictionary<string, IEnumerable<string>>
            {
                {columnName, errors[columnName]}
            };
            
        }
    }
}