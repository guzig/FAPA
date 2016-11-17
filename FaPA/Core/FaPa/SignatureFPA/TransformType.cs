using System.Xml.Serialization;

namespace FaPA.Core.FaPa.SignatureFPA
{
    
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class TransformType {
        
        private object[] itemsField;
        
        private string[] textField;
        
        private string algorithmField;
        
        
        [XmlAnyElement]
        [XmlElement("XPath", typeof(string))]
        public object[] Items {
            get {
                return itemsField;
            }
            set {
                itemsField = value;
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
        
        
        [XmlAttribute(DataType="anyURI")]
        public string Algorithm {
            get {
                return algorithmField;
            }
            set {
                algorithmField = value;
            }
        }
    }
}