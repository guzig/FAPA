using System;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DatiAnagraficiRappresentanteType : BaseEntityFpa
    {
        private IdFiscaleType _idFiscaleIvaField;
        private string _codiceFiscaleField;
        private AnagraficaType _anagraficaField;
        
        public virtual IdFiscaleType IdFiscaleIVA
        {
            get
            {
                return _idFiscaleIvaField;
            }
            set
            {
                _idFiscaleIvaField = value;
            }
        }
        
        public virtual string CodiceFiscale
        {
            get
            {
                return _codiceFiscaleField;
            }
            set
            {
                _codiceFiscaleField = value;
            }
        }
        
        public virtual AnagraficaType Anagrafica
        {
            get
            {
                return _anagraficaField;
            }
            set
            {
                _anagraficaField = value;
            }
        }
    }
}