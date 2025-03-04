using NHibernate.Validator.Cfg;
using NHibernate.Validator.Engine;

namespace FaPA.DomainServices.Helpers
{
    public class BasicSharedEngineProvider : ISharedEngineProvider
    {
        
        private readonly ValidatorEngine _validatorEngine;

        public BasicSharedEngineProvider(ValidatorEngine validatorEngine)
        {
            this._validatorEngine = validatorEngine;
        }

        public ValidatorEngine GetEngine()
        {
            return _validatorEngine;
        }

        public void UseMe()
        {
            Environment.SharedEngineProvider = this;
        }

    }
}
