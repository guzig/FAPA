using System.Collections.Generic;

namespace FaPA.AppServices.CoreValidation
{
    public class NhFornitoreValidator : NhInstanceValidator
    {
        protected override IEnumerable<string> ItemLevelValidationGroup => new[]
        {
            "PIva", "Denominazione"
        };
    }
}