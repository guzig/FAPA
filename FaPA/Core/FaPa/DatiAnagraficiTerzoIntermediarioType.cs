using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DatiAnagraficiTerzoIntermediarioType : BaseEntityFpa
    {
        private IdFiscaleType _idFiscaleIvaField;
        private string _codiceFiscaleField;
        private AnagraficaType _anagraficaField;
        
        [XmlElement( Form = XmlSchemaForm.Unqualified )]
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
        
        [XmlElement( Form = XmlSchemaForm.Unqualified )]
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