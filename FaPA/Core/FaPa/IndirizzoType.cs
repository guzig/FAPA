using System;

namespace FaPA.Core.FaPa
{
    [Serializable]
    //[XmlType( Namespace = "http://www.fatturapa.gov.it/sdi/fatturapa/v1.1" )]
    public class IndirizzoType : BaseEntityFpa
    {

        #region fields

        private string indirizzoField;

        private string numeroCivicoField;

        private string cAPField;

        private string comuneField;

        private string provinciaField;

        private string nazioneField;

        #endregion

        public IndirizzoType()
        {
            nazioneField = "IT";
        }

        public virtual  string Indirizzo
        {
            get
            {
                return indirizzoField;
            }
            set
            {
                indirizzoField = value;
            }
        }

        public virtual  string NumeroCivico
        {
            get
            {
                return numeroCivicoField;
            }
            set
            {
                numeroCivicoField = value;
            }
        }

        public virtual  string CAP
        {
            get
            {
                return cAPField;
            }
            set
            {
                cAPField = value;
            }
        }

        public virtual  string Comune
        {
            get
            {
                return comuneField;
            }
            set
            {
                comuneField = value;
            }
        }

        public virtual  string Provincia
        {
            get
            {
                return provinciaField;
            }
            set
            {
                provinciaField = value;
            }
        }

        public virtual  string Nazione
        {
            get
            {
                return nazioneField;
            }
            set
            {
                nazioneField = value;
            }
        }
    }
}