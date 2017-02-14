using System;
using FaPA.Core.FaPa;

namespace FaPA.Core
{
    [Serializable]
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

            var anag = new CrossPropertiesValidationContext<AnagraficaType>().
                AddCrossCoupledPropValidation( c => c.Denominazione ).
                AddCrossCoupledPropValidation( c => c.Cognome ).
                AddCrossCoupledPropValidation( c => c.Nome );

            AddCrossCoupledPropValidationContext( anag );
        }
    }
}