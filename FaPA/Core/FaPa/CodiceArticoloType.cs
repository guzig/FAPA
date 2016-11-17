using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class CodiceArticoloType
    {
        private string codiceTipoField;
        private string codiceValoreField;
        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "normalizedString" )]
        public virtual string CodiceTipo
        {
            get
            {
                return codiceTipoField;
            }
            set
            {
                codiceTipoField = value;
            }
        }
        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "normalizedString" )]
        public virtual string CodiceValore
        {
            get
            {
                return codiceValoreField;
            }
            set
            {
                codiceValoreField = value;
            }
        }
    }
}