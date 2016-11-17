using System.Xml.Serialization;

namespace FaPA.Core.FaPa.SignatureFPA
{
    
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class RetrievalMethodType {
        
        private TransformType[] transformsField;
        
        private string uRIField;
        
        private string typeField;
        
        
        [XmlArrayItem("Transform", IsNullable=false)]
        public TransformType[] Transforms {
            get {
                return transformsField;
            }
            set {
                transformsField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string URI {
            get {
                return uRIField;
            }
            set {
                uRIField = value;
            }
        }
        
        
        [XmlAttribute(DataType="anyURI")]
        public string Type {
            get {
                return typeField;
            }
            set {
                typeField = value;
            }
        }
    }
}