using System.Xml.Serialization;

namespace FaPA.Core.FaPa.SignatureFPA
{
    
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class SignedInfoType {
        
        private CanonicalizationMethodType canonicalizationMethodField;
        
        private SignatureMethodType signatureMethodField;
        
        private ReferenceType[] referenceField;
        
        private string idField;
        
        
        public CanonicalizationMethodType CanonicalizationMethod {
            get {
                return canonicalizationMethodField;
            }
            set {
                canonicalizationMethodField = value;
            }
        }
        
        
        public SignatureMethodType SignatureMethod {
            get {
                return signatureMethodField;
            }
            set {
                signatureMethodField = value;
            }
        }
        
        
        [XmlElement("Reference")]
        public ReferenceType[] Reference {
            get {
                return referenceField;
            }
            set {
                referenceField = value;
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
    }
}