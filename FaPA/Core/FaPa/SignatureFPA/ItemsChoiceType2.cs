using System.Xml.Serialization;

namespace FaPA.Core.FaPa.SignatureFPA
{
    
    [XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#", IncludeInSchema=false)]
    public enum ItemsChoiceType2 {
        
        
        [XmlEnum("##any:")]
        Item,
        
        
        PGPKeyID,
        
        
        PGPKeyPacket
    }
}