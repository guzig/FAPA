namespace FaPA.Core
{
    public class CustomCrossPropertiesValidationResolver : CrossPropertiesValidationResolver
    {
        //ctor
        public CustomCrossPropertiesValidationResolver()
        {
            var cpContext = new CrossPropertiesValidationContext<Anagrafica>().
                AddCrossCoupledPropValidation( c => c.PIva ).
                AddCrossCoupledPropValidation( c => c.CodiceFiscale ).
                AddCrossCoupledPropValidation( c => c.PIva ).
                AddCrossCoupledPropValidation( c => c.Denominazione ).
                AddCrossCoupledPropValidation( c => c.Cognome ).
                AddCrossCoupledPropValidation( c => c.Nome );

            AddCrossCoupledPropValidationContext( cpContext );

            var fattCt = new CrossPropertiesValidationContext<Fattura>().
                AddCrossCoupledPropValidation( c => c.NumeroFatturaDB ).
                AddCrossCoupledPropValidation( c => c.DataFatturaDB ).
                AddCrossCoupledPropValidation( c => c.AnagraficaCedenteDB );

            AddCrossCoupledPropValidationContext( fattCt  );
        }
    }
}