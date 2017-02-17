using System;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    
    //[XmlRoot("FatturaElettronica", Namespace = "http://www.fatturapa.gov.it/sdi/fatturapa/v1.1", IsNullable = false)]
    public class DatiTrasmissioneType : BaseEntityFpa
    {
        private IdFiscaleType _idTrasmittenteField;
        private string _progressivoInvioField;
        private FormatoTrasmissioneType _formatoTrasmissioneField;
        private string _codiceDestinatarioField;
        private ContattiTrasmittenteType _contattiTrasmittenteField;
        private string _pecDestinatarioField;

        public string PECDestinatario
        {
            get { return _pecDestinatarioField; }
            set
            {
                _pecDestinatarioField = value;
            }
        }

        [XmlIgnore]
        public bool PECDestinatarioSpecified { get; set; } = false;

        public virtual IdFiscaleType IdTrasmittente
        {
            get
            {
                return _idTrasmittenteField;
            }
            set
            {
                _idTrasmittenteField = value;
            }
        }

        public virtual string ProgressivoInvio
        {
            get
            {
                return _progressivoInvioField;
            }
            set
            {
                _progressivoInvioField = value;
            }
        }

        public virtual FormatoTrasmissioneType FormatoTrasmissione
        {
            get
            {
                return _formatoTrasmissioneField;
            }
            set
            {
                _formatoTrasmissioneField = value;
                PECDestinatarioSpecified = _formatoTrasmissioneField == FormatoTrasmissioneType.FPR12;
            }
        }

        public virtual string CodiceDestinatario
        {
            get
            {
                return _codiceDestinatarioField;
            }
            set
            {
                _codiceDestinatarioField = value;
            }
        }

        public virtual ContattiTrasmittenteType ContattiTrasmittente
        {
            get
            {
                return _contattiTrasmittenteField;
            }
            set
            {
                _contattiTrasmittenteField = value;
            }
        }
    }
}