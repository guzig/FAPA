using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class ContattiTrasmittenteType : BaseEntityFpa
    {
        private string _telefonoField;
        private string _emailField;
        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "normalizedString" )]
        public virtual string Telefono
        {
            get
            {
                return _telefonoField;
            }
            set
            {
                _telefonoField = value;
            }
        }
        
        [XmlElement( Form = XmlSchemaForm.Unqualified )]
        public virtual string Email
        {
            get
            {
                return _emailField;
            }
            set
            {
                _emailField = value;
            }
        }
    }
}