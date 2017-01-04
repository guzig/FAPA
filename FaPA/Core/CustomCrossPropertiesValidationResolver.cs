namespace FaPA.Core
{
    public class CustomCrossPropertiesValidationResolver : CrossPropertiesValidationResolver
    {
        //ctor
        public CustomCrossPropertiesValidationResolver()
        {
            var cpContext = new CrossPropertiesValidationContext<Committente>().
                AddCrossCoupledPropValidation( c => c.PIva ).
                AddCrossCoupledPropValidation( c => c.CodiceFiscale ).
                AddCrossCoupledPropValidation( c => c.PIva ).
                AddCrossCoupledPropValidation( c => c.Denominazione ).
                AddCrossCoupledPropValidation( c => c.Cognome ).
                AddCrossCoupledPropValidation( c => c.Nome );

            AddCrossCoupledPropValidation( cpContext );
        }
    }
}