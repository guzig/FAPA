using System.Collections.Generic;

namespace FaPA.AppServices.CoreValidation
{
    public class NhCommittenteValidator : NhInstanceValidator
    {
        protected override IEnumerable<string> ItemLevelValidationGroup => new[]
        {
            "CodiceFiscale", "PIva", "Denominazione", "CodiceFiscale", "Cognome", "Nome"
        };
    }
}