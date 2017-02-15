using System;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    //[XmlType( Namespace = "http://www.fatturapa.gov.it/sdi/fatturapa/v1.1" )]
    //[XmlRoot( "FatturaElettronica", Namespace = "http://www.fatturapa.gov.it/sdi/fatturapa/v1.1", IsNullable = false )]
    //[XmlTypeAttribute(AnonymousType = true, Namespace = "")]
    public class FatturaElettronicaHeaderType : BaseEntityFpa
    {
        private DatiTrasmissioneType _datiTrasmissioneField;
        private CedentePrestatoreType _cedentePrestatoreField;
        private RappresentanteFiscaleType _rappresentanteFiscaleField;
        private CessionarioCommittenteType _cessionarioCommittenteField;
        private TerzoIntermediarioSoggettoEmittenteType _terzoIntermediarioOSoggettoEmittenteField;
        private SoggettoEmittenteType _soggettoEmittenteField;
        private bool _soggettoEmittenteFieldSpecified;

        public virtual  DatiTrasmissioneType DatiTrasmissione
        {
            get
            {
                return _datiTrasmissioneField;
            }
            set
            {
                _datiTrasmissioneField = value;
            }
        }

        public virtual  CedentePrestatoreType CedentePrestatore
        {
            get
            {
                return _cedentePrestatoreField;
            }
            set
            {
                _cedentePrestatoreField = value;
            }
        }

        public virtual  RappresentanteFiscaleType RappresentanteFiscale
        {
            get
            {
                return _rappresentanteFiscaleField;
            }
            set
            {
                _rappresentanteFiscaleField = value;
            }
        }

        public virtual  CessionarioCommittenteType CessionarioCommittente
        {
            get
            {
                return _cessionarioCommittenteField;
            }
            set
            {
                _cessionarioCommittenteField = value;
            }
        }

        public virtual  TerzoIntermediarioSoggettoEmittenteType TerzoIntermediarioOSoggettoEmittente
        {
            get
            {
                return _terzoIntermediarioOSoggettoEmittenteField;
            }
            set
            {
                _terzoIntermediarioOSoggettoEmittenteField = value;
                SoggettoEmittenteSpecified = _terzoIntermediarioOSoggettoEmittenteField != null;
            }
        }

        public virtual  SoggettoEmittenteType SoggettoEmittente
        {
            get
            {
                return _soggettoEmittenteField;
            }
            set
            {
                _soggettoEmittenteField = value;
            }
        }

        [XmlIgnore]
        public virtual bool SoggettoEmittenteSpecified
        {
            get
            {
                return _soggettoEmittenteFieldSpecified;
            }
            set
            {
                _soggettoEmittenteFieldSpecified = value;
            }
        }
    }
}