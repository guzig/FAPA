using System.Xml.Serialization;

namespace FaPA.Core.FaPa.SignatureFPA
{
    
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class KeyValueType {
        
        private object itemField;
        
        private string[] textField;
        
        
        [XmlAnyElement]
        [XmlElement("DSAKeyValue", typeof(DSAKeyValueType))]
        [XmlElement("RSAKeyValue", typeof(RSAKeyValueType))]
        public object Item {
            get {
                return itemField;
            }
            set {
                itemField = value;
            }
        }
        
        
        [XmlText]
        public string[] Text {
            get {
                return textField;
            }
            set {
                textField = value;
            }
        }
    }
}