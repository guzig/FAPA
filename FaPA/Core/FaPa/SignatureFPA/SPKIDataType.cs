using System.Xml;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa.SignatureFPA
{
    
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class SPKIDataType {
        
        private byte[][] sPKISexpField;
        
        private XmlElement anyField;
        
        
        [XmlElement("SPKISexp", DataType="base64Binary")]
        public byte[][] SPKISexp {
            get {
                return sPKISexpField;
            }
            set {
                sPKISexpField = value;
            }
        }
        
        
        [XmlAnyElement]
        public XmlElement Any {
            get {
                return anyField;
            }
            set {
                anyField = value;
            }
        }
    }
}