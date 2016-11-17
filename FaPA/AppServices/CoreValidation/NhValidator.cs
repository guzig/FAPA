using System.Collections.Generic;
using System.Linq;
using FaPA.DomainServices.Utils;
using NHibernate.Validator.Engine;

namespace FaPA.AppServices.CoreValidation
{
    public abstract class NhValidator : BaseCoreValidator
    {
        protected static readonly ValidatorEngine Validator = NHibernate.Validator.Cfg.Environment.SharedEngineProvider?.GetEngine();

        public override IDictionary<string, IEnumerable<string>> GetValidationErrors(object instance)
        {
            return Validator.Validate(instance).Where( m => !string.IsNullOrWhiteSpace(m.Message) ).
                GroupBy(g=>g.PropertyName).ToDictionary(k=>k.Key, v=>v.Select(m=>m.Message) );
        }

        public override IDictionary<string, IEnumerable<string>> GetValidationErrors(string columnName, object instance)
        {
            var errors = Validator.ValidatePropertyValue(instance, columnName).
                DistinctBy(d => d.Message).Select(d=>d.Message).ToList();

            return new Dictionary<string, IEnumerable<string>> { { columnName, errors } };
        }
    }
}