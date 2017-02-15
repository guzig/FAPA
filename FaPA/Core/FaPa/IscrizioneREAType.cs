using System;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class IscrizioneREAType : BaseEntityFpa
    {

        private string _ufficioField;

        private string _numeroReaField;

        private decimal _capitaleSocialeField;

        private bool _capitaleSocialeFieldSpecified;

        private SocioUnicoType _socioUnicoField;

        private bool _socioUnicoFieldSpecified;

        private StatoLiquidazioneType _statoLiquidazioneField;

        public virtual  string Ufficio
        {
            get
            {
                return _ufficioField;
            }
            set
            {
                _ufficioField = value;
            }
        }

        public virtual  string NumeroREA
        {
            get
            {
                return _numeroReaField;
            }
            set
            {
                _numeroReaField = value;
            }
        }

        public virtual  decimal CapitaleSociale
        {
            get
            {
                return _capitaleSocialeField;
            }
            set
            {
                _capitaleSocialeField = value;
            }
        }

        [XmlIgnore]
        public virtual  bool CapitaleSocialeSpecified
        {
            get
            {
                return _capitaleSocialeFieldSpecified;
            }
            set
            {
                _capitaleSocialeFieldSpecified = value;
            }
        }

        public virtual  SocioUnicoType SocioUnico
        {
            get
            {
                return _socioUnicoField;
            }
            set
            {
                _socioUnicoField = value;
            }
        }

        [XmlIgnore]
        public virtual  bool SocioUnicoSpecified
        {
            get
            {
                return _socioUnicoFieldSpecified;
            }
            set
            {
                _socioUnicoFieldSpecified = value;
            }
        }

        public virtual  StatoLiquidazioneType StatoLiquidazione
        {
            get
            {
                return _statoLiquidazioneField;
            }
            set
            {
                _statoLiquidazioneField = value;
            }
        }
    }
}