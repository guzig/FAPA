using System;
using System.Collections.Generic;
using System.Linq;


namespace FaPA.Core
{
    [Serializable]
    public class DomainResult
    {
        public DomainResult(IDictionary<string, IEnumerable<string>> errors )
        {
            if ( errors == null || !errors.Any() )
                Success = true;
            else
                Success = false;

            Errors = errors;
        }

        public DomainResult(bool success, IDictionary<string, IEnumerable<string>> errors = null)
        {
            Success = success;
            Errors = errors;
        }

        public bool Success { get; }

        public IDictionary<string, IEnumerable<string>> Errors { get; }
    }
}