using System.Xml.Serialization;

namespace FaPA.Core.FaPa.SignatureFPA
{
    
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class PGPDataType {
        
        private object[] itemsField;
        
        private ItemsChoiceType2[] itemsElementNameField;
        
        
        [XmlAnyElement]
        [XmlElement("PGPKeyID", typeof(byte[]), DataType="base64Binary")]
        [XmlElement("PGPKeyPacket", typeof(byte[]), DataType="base64Binary")]
        [XmlChoiceIdentifier("ItemsElementName")]
        public object[] Items {
            get {
                return itemsField;
            }
            set {
                itemsField = value;
            }
        }
        
        
        [XmlElement("ItemsElementName")]
        [XmlIgnore]
        public ItemsChoiceType2[] ItemsElementName {
            get {
                return itemsElementNameField;
            }
            set {
                itemsElementNameField = value;
            }
        }
    }
}