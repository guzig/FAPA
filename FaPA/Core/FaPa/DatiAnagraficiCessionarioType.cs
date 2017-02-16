using System;

namespace FaPA.Core.FaPa
{
    [Serializable]
    
    public class DatiAnagraficiCessionarioType: BaseEntityFpa
    {
        private IdFiscaleType idFiscaleIVAField;
        private string codiceFiscaleField;
        private AnagraficaType anagraficaField;

        public virtual IdFiscaleType IdFiscaleIVA
        {
            get
            {
                return idFiscaleIVAField;
            }
            set
            {
                idFiscaleIVAField = value;
            }
        }

        public virtual string CodiceFiscale
        {
            get
            {
                return codiceFiscaleField;
            }
            set
            {
                codiceFiscaleField = value;
            }
        }

        public virtual AnagraficaType Anagrafica
        {
            get
            {
                return anagraficaField;
            }
            set
            {
                anagraficaField = value;
            }
        }
    }
}