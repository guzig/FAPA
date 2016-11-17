using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DatiAnagraficiVettoreType : BaseEntityFpa
    {
        private IdFiscaleType idFiscaleIVAField;
        private string codiceFiscaleField;
        private AnagraficaType anagraficaField;
        private string numeroLicenzaGuidaField;

        [XmlElement( Form = XmlSchemaForm.Unqualified )]
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

        [XmlElement( Form = XmlSchemaForm.Unqualified )]
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
        
        [XmlElement( Form = XmlSchemaForm.Unqualified )]
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
        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "normalizedString" )]
        public virtual string NumeroLicenzaGuida
        {
            get
            {
                return numeroLicenzaGuidaField;
            }
            set
            {
                numeroLicenzaGuidaField = value;
            }
        }
    }
}