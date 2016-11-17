using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DatiBolloType : BaseEntityFpa
    {
        private BolloVirtualeType bolloVirtualeField;
        private decimal importoBolloField;
        
        [XmlElement( Form = XmlSchemaForm.Unqualified )]
        public virtual BolloVirtualeType BolloVirtuale
        {
            get
            {
                return bolloVirtualeField;
            }
            set
            {
                bolloVirtualeField = value;
            }
        }
        
        [XmlElement( Form = XmlSchemaForm.Unqualified )]
        public virtual decimal ImportoBollo
        {
            get
            {
                return importoBolloField;
            }
            set
            {
                importoBolloField = value;
            }
        }
    }
}