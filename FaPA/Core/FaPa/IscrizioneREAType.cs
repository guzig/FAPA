using System;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class IscrizioneREAType : BaseEntityFpa
    {

        private string ufficioField;

        private string numeroREAField;

        private decimal capitaleSocialeField;

        private bool capitaleSocialeFieldSpecified;

        private SocioUnicoType socioUnicoField;

        private bool socioUnicoFieldSpecified;

        private StatoLiquidazioneType statoLiquidazioneField;

        public virtual  string Ufficio
        {
            get
            {
                return ufficioField;
            }
            set
            {
                ufficioField = value;
            }
        }

        public virtual  string NumeroREA
        {
            get
            {
                return numeroREAField;
            }
            set
            {
                numeroREAField = value;
            }
        }

        public virtual  decimal CapitaleSociale
        {
            get
            {
                return capitaleSocialeField;
            }
            set
            {
                capitaleSocialeField = value;
            }
        }

        [XmlIgnore]
        public virtual  bool CapitaleSocialeSpecified
        {
            get
            {
                return capitaleSocialeFieldSpecified;
            }
            set
            {
                capitaleSocialeFieldSpecified = value;
            }
        }

        public virtual  SocioUnicoType SocioUnico
        {
            get
            {
                return socioUnicoField;
            }
            set
            {
                socioUnicoField = value;
            }
        }

        [XmlIgnore]
        public virtual  bool SocioUnicoSpecified
        {
            get
            {
                return socioUnicoFieldSpecified;
            }
            set
            {
                socioUnicoFieldSpecified = value;
            }
        }

        public virtual  StatoLiquidazioneType StatoLiquidazione
        {
            get
            {
                return statoLiquidazioneField;
            }
            set
            {
                statoLiquidazioneField = value;
            }
        }
    }
}