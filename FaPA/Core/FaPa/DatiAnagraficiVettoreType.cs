using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DatiAnagraficiVettoreType : BaseEntityFpa
    {
        private IdFiscaleType _idFiscaleIvaField;
        private string _codiceFiscaleField;
        private AnagraficaType _anagraficaField;
        private string _numeroLicenzaGuidaField;

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

        [XmlElement( Form = XmlSchemaForm.Unqualified )]
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
        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "normalizedString" )]
        public virtual string NumeroLicenzaGuida
        {
            get
            {
                return _numeroLicenzaGuidaField;
            }
            set
            {
                _numeroLicenzaGuidaField = value;
            }
        }
    }
}