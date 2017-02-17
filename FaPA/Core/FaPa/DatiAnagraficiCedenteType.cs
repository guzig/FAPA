using System;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DatiAnagraficiCedenteType: BaseEntityFpa
    {
        private IdFiscaleType _idFiscaleIvaField;
        private string _codiceFiscaleField;
        private AnagraficaType _anagraficaField;
        private string _alboProfessionaleField;
        private string _provinciaAlboField;
        private string _numeroIscrizioneAlboField;
        private DateTime _dataIscrizioneAlboField;
        private bool _dataIscrizioneAlboFieldSpecified;
        private RegimeFiscaleType _regimeFiscaleField;

        public virtual IdFiscaleType IdFiscaleIVA
        {
            get
            {
                return _idFiscaleIvaField;
            }
            set
            {
                _idFiscaleIvaField = value;
            }
        }

        public virtual string CodiceFiscale
        {
            get
            {
                return _codiceFiscaleField;
            }
            set
            {
                _codiceFiscaleField = value;
            }
        }

        public virtual AnagraficaType Anagrafica
        {
            get
            {
                return _anagraficaField;
            }
            set
            {
                _anagraficaField = value;
            }
        }

        public virtual string AlboProfessionale
        {
            get
            {
                return _alboProfessionaleField;
            }
            set
            {
                _alboProfessionaleField = value;
            }
        }

        public virtual string ProvinciaAlbo
        {
            get
            {
                return _provinciaAlboField;
            }
            set
            {
                _provinciaAlboField = value;
            }
        }

        public virtual string NumeroIscrizioneAlbo
        {
            get
            {
                return _numeroIscrizioneAlboField;
            }
            set
            {
                _numeroIscrizioneAlboField = value;
            }
        }

        public virtual DateTime DataIscrizioneAlbo
        {
            get
            {
                return _dataIscrizioneAlboField;
            }
            set
            {
                _dataIscrizioneAlboField = value;
            }
        }

        [XmlIgnore]
        public virtual bool DataIscrizioneAlboSpecified
        {
            get
            {
                return _dataIscrizioneAlboFieldSpecified;
            }
            set
            {
                _dataIscrizioneAlboFieldSpecified = value;
            }
        }

        public virtual RegimeFiscaleType RegimeFiscale
        {
            get
            {
                return _regimeFiscaleField;
            }
            set
            {
                _regimeFiscaleField = value;
            }
        }
    }
}