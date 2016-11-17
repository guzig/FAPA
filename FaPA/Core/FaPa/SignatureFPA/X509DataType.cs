using System.Xml.Serialization;

namespace FaPA.Core.FaPa.SignatureFPA
{
    
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class X509DataType {
        
        private object[] itemsField;
        
        private ItemsChoiceType1[] itemsElementNameField;
        
        
        [XmlAnyElement]
        [XmlElement("X509CRL", typeof(byte[]), DataType="base64Binary")]
        [XmlElement("X509Certificate", typeof(byte[]), DataType="base64Binary")]
        [XmlElement("X509IssuerSerial", typeof(X509IssuerSerialType))]
        [XmlElement("X509SKI", typeof(byte[]), DataType="base64Binary")]
        [XmlElement("X509SubjectName", typeof(string))]
        [XmlChoiceIdentifier("ItemsElementName")]
        public object[] Items {
            get {
                return itemsField;
            }
            set {
                itemsField = value;
            }
        }
        
        
        [XmlElement("ItemsElementName")]
        [XmlIgnore]
        public ItemsChoiceType1[] ItemsElementName {
            get {
                return itemsElementNameField;
            }
            set {
                itemsElementNameField = value;
            }
        }
    }
}