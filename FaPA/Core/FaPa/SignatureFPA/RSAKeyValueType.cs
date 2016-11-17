using System.Xml.Serialization;

namespace FaPA.Core.FaPa.SignatureFPA
{
    
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class RSAKeyValueType {
        
        private byte[] modulusField;
        
        private byte[] exponentField;
        
        
        [XmlElement(DataType="base64Binary")]
        public byte[] Modulus {
            get {
                return modulusField;
            }
            set {
                modulusField = value;
            }
        }
        
        
        [XmlElement(DataType="base64Binary")]
        public byte[] Exponent {
            get {
                return exponentField;
            }
            set {
                exponentField = value;
            }
        }
    }
}