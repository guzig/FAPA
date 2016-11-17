using System.Xml;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa.SignatureFPA
{
    
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class SignatureMethodType {
        
        private string hMACOutputLengthField;
        
        private XmlNode[] anyField;
        
        private string algorithmField;
        
        
        [XmlElement(DataType="integer")]
        public string HMACOutputLength {
            get {
                return hMACOutputLengthField;
            }
            set {
                hMACOutputLengthField = value;
            }
        }
        
        
        [XmlText]
        [XmlAnyElement]
        public XmlNode[] Any {
            get {
                return anyField;
            }
            set {
                anyField = value;
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