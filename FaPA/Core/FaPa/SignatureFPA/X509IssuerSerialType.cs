using System.Xml.Serialization;

namespace FaPA.Core.FaPa.SignatureFPA
{
    
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class X509IssuerSerialType {
        
        private string x509IssuerNameField;
        
        private string x509SerialNumberField;
        
        
        public string X509IssuerName {
            get {
                return x509IssuerNameField;
            }
            set {
                x509IssuerNameField = value;
            }
        }
        
        
        [XmlElement(DataType="integer")]
        public string X509SerialNumber {
            get {
                return x509SerialNumberField;
            }
            set {
                x509SerialNumberField = value;
            }
        }
    }
}