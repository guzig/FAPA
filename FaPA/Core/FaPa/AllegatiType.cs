using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    //[XmlType( Namespace = "http://www.fatturapa.gov.it/sdi/fatturapa/v1.1" )]
    public class AllegatiType: BaseEntityFpa
    {
        private string _nomeAttachmentField;
        private string _algoritmoCompressioneField;
        private string _formatoAttachmentField;
        private string _descrizioneAttachmentField;
        private byte[] _attachmentField;
        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "normalizedString" )]
        public virtual string NomeAttachment
        {
            get
            {
                return _nomeAttachmentField;
            }
            set
            {
                _nomeAttachmentField = value;
            }
        }
        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "normalizedString" )]
        public virtual string AlgoritmoCompressione
        {
            get
            {
                return _algoritmoCompressioneField;
            }
            set
            {
                _algoritmoCompressioneField = value;
            }
        }
        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "normalizedString" )]
        public virtual string FormatoAttachment
        {
            get
            {
                return _formatoAttachmentField;
            }
            set
            {
                _formatoAttachmentField = value;
            }
        }
        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "normalizedString" )]
        public virtual string DescrizioneAttachment
        {
            get
            {
                return _descrizioneAttachmentField;
            }
            set
            {
                _descrizioneAttachmentField = value;
            }
        }
        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "base64Binary" )]
        public virtual byte[] Attachment
        {
            get
            {
                return _attachmentField;
            }
            set
            {
                _attachmentField = value;
            }
        }
    }
}