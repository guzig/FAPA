using System.Collections.Generic;

namespace FaPA.AppServices.CoreValidation
{
    public interface ICoreValidator
    {
        IDictionary<string, IEnumerable<string>> GetValidationErrors(object instance);
        IDictionary<string, IEnumerable<string>> GetValidationErrors(string columnName, object instance);
    }
}
