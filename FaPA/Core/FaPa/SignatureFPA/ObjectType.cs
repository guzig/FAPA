using System.Xml;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa.SignatureFPA
{
    
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class ObjectType {
        
        private XmlNode[] anyField;
        
        private string idField;
        
        private string mimeTypeField;
        
        private string encodingField;
        
        
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
        
        
        [XmlAttribute(DataType="ID")]
        public string Id {
            get {
                return idField;
            }
            set {
                idField = value;
            }
        }
        
        
        [XmlAttribute]
        public string MimeType {
            get {
                return mimeTypeField;
            }
            set {
                mimeTypeField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string Encoding {
            get {
                return encodingField;
            }
            set {
                encodingField = value;
            }
        }
    }
}