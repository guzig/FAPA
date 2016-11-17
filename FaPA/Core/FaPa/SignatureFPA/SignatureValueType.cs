using System.Xml.Serialization;

namespace FaPA.Core.FaPa.SignatureFPA
{
    
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class SignatureValueType {
        
        private string idField;
        
        private byte[] valueField;
        
        
        [XmlAttribute(DataType="ID")]
        public string Id {
            get {
                return idField;
            }
            set {
                idField = value;
            }
        }
        
        
        [XmlText(DataType="base64Binary")]
        public byte[] Value {
            get {
                return valueField;
            }
            set {
                valueField = value;
            }
        }
    }
}