using System;
using System.Xml.Serialization;
using FaPA.Core.FaPa.SignatureFPA;

namespace FaPA.Core.FaPa
{
    [Serializable]
    //[XmlType( Namespace = "http://www.fatturapa.gov.it/sdi/fatturapa/v1.1" )]
    [XmlRoot( "FatturaElettronica", Namespace = "http://www.fatturapa.gov.it/sdi/fatturapa/v1.1", IsNullable = false )]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "")]
    public class FatturaElettronicaType : BaseEntityFpa
    {
        private FatturaElettronicaHeaderType fatturaElettronicaHeaderField;

        public virtual  FatturaElettronicaHeaderType FatturaElettronicaHeader
        {
            get
            {
                return fatturaElettronicaHeaderField;
            }
            set
            {
                fatturaElettronicaHeaderField = value;
            }
        }

        private FatturaElettronicaBodyType fatturaElettronicaBodyField;
        public virtual  FatturaElettronicaBodyType FatturaElettronicaBody
        {
            get
            {
                return fatturaElettronicaBodyField;
            }
            set
            {
                fatturaElettronicaBodyField = value;
            }
        }

        private SignatureType signatureField;
        
        [XmlElement( Namespace = "http://www.w3.org/2000/09/xmldsig#" )]
        public virtual  SignatureType Signature
        {
            get
            {
                return signatureField;
            }
            set
            {
                signatureField = value;
            }
        }
        
        private VersioneSchemaType versioneField;

        [XmlAttribute]
        public virtual  VersioneSchemaType versione
        {
            get
            {
                return versioneField;
            }
            set
            {
                versioneField = value;
            }
        }

        public FatturaElettronicaType DeepCopy()
        {
            return this.CopyDeep();
        }
    }
}

