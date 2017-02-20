using System.Collections.Generic;

namespace FaPA.AppServices.CoreValidation
{
    public interface ICoreValidator
    {
        IDictionary<string, List<string>> GetValidationErrors(object instance);
        IDictionary<string, List<string>> GetValidationErrors(string columnName, object instance);
    }
}
