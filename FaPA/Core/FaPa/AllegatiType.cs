using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    //[XmlType( Namespace = "http://www.fatturapa.gov.it/sdi/fatturapa/v1.1" )]
    public class AllegatiType
    {
        private string nomeAttachmentField;
        private string algoritmoCompressioneField;
        private string formatoAttachmentField;
        private string descrizioneAttachmentField;
        private byte[] attachmentField;
        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "normalizedString" )]
        public virtual string NomeAttachment
        {
            get
            {
                return nomeAttachmentField;
            }
            set
            {
                nomeAttachmentField = value;
            }
        }
        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "normalizedString" )]
        public virtual string AlgoritmoCompressione
        {
            get
            {
                return algoritmoCompressioneField;
            }
            set
            {
                algoritmoCompressioneField = value;
            }
        }
        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "normalizedString" )]
        public virtual string FormatoAttachment
        {
            get
            {
                return formatoAttachmentField;
            }
            set
            {
                formatoAttachmentField = value;
            }
        }
        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "normalizedString" )]
        public virtual string DescrizioneAttachment
        {
            get
            {
                return descrizioneAttachmentField;
            }
            set
            {
                descrizioneAttachmentField = value;
            }
        }
        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "base64Binary" )]
        public virtual byte[] Attachment
        {
            get
            {
                return attachmentField;
            }
            set
            {
                attachmentField = value;
            }
        }
    }
}