using System;
using FaPA.AppServices.CoreValidation;

namespace FaPA.Core
{
    public abstract class BaseEntityFpa: BaseEntity
    {
        protected BaseEntityFpa()
        {
            Id = Guid.NewGuid().GetHashCode();
        }

        public override DomainResult Validate()
        {
            var er = CoreValidatorService.GetValidationErrors(this);
            DomainResult =  new DomainResult(er);
            return DomainResult;
        }

        public override DomainResult ValidatePropertyValue( string prop )
        {
            return Validate();
        }

        protected override bool IsTransient()
        {
            return true;
        }

    }
}