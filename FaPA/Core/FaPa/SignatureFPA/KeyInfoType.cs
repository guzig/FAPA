using System.Xml.Serialization;

namespace FaPA.Core.FaPa.SignatureFPA
{
    
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public class KeyInfoType {
        
        private object[] itemsField;
        
        private ItemsChoiceType3[] itemsElementNameField;
        
        private string[] textField;
        
        private string idField;
        
        
        [XmlAnyElement]
        [XmlElement("KeyName", typeof(string))]
        [XmlElement("KeyValue", typeof(KeyValueType))]
        [XmlElement("MgmtData", typeof(string))]
        [XmlElement("PGPData", typeof(PGPDataType))]
        [XmlElement("RetrievalMethod", typeof(RetrievalMethodType))]
        [XmlElement("SPKIData", typeof(SPKIDataType))]
        [XmlElement("X509Data", typeof(X509DataType))]
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
        public ItemsChoiceType3[] ItemsElementName {
            get {
                return itemsElementNameField;
            }
            set {
                itemsElementNameField = value;
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