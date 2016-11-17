using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class ContattiType
    {
        private string telefonoField;
        private string faxField;
        private string emailField;
        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "normalizedString" )]
        public virtual string Telefono
        {
            get
            {
                return telefonoField;
            }
            set
            {
                telefonoField = value;
            }
        }

        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "normalizedString" )]
        public virtual string Fax
        {
            get
            {
                return faxField;
            }
            set
            {
                faxField = value;
            }
        }
        
        [XmlElement( Form = XmlSchemaForm.Unqualified )]
        public virtual string Email
        {
            get
            {
                return emailField;
            }
            set
            {
                emailField = value;
            }
        }
    }
}