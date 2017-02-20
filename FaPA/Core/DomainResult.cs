using System.Collections.Generic;
using System.Linq;


namespace FaPA.Core
{
    public class DomainResult
    {
        public DomainResult(IDictionary<string, List<string>> errors )
        {
            if ( errors == null || !errors.Any() )
                Success = true;
            else
                Success = false;

            Errors = errors;
        }

        public DomainResult(bool success, IDictionary<string, List<string>> errors = null)
        {
            Success = success;
            Errors = errors;
        }

        public bool Success { get; }

        public IDictionary<string, List<string>> Errors { get; }
    }
}