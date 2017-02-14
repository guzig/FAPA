using System;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    //[XmlType( Namespace = "http://www.fatturapa.gov.it/sdi/fatturapa/v1.1" )]
    public class CedentePrestatoreType : BaseEntityFpa
    {
        #region fields

        private DatiAnagraficiCedenteType datiAnagraficiField;

        private IndirizzoType sedeField;

        private IndirizzoType stabileOrganizzazioneField;

        private IscrizioneREAType iscrizioneREAField;

        private ContattiType contattiField;

        private string riferimentoAmministrazioneField;

        #endregion

        public virtual DatiAnagraficiCedenteType DatiAnagrafici
        {
            get
            {
                return datiAnagraficiField;
            }
            set
            {
                datiAnagraficiField = value;
            }
        }

        public virtual IndirizzoType Sede
        {
            get
            {
                return sedeField;
            }
            set
            {
                sedeField = value;
            }
        }

        public virtual IndirizzoType StabileOrganizzazione
        {
            get
            {
                return stabileOrganizzazioneField;
            }
            set
            {
                stabileOrganizzazioneField = value;
            }
        }

        public virtual IscrizioneREAType IscrizioneREA
        {
            get
            {
                return iscrizioneREAField;
            }
            set
            {
                iscrizioneREAField = value;
            }
        }

        public virtual ContattiType Contatti
        {
            get
            {
                return contattiField;
            }
            set
            {
                contattiField = value;
            }
        }

        public virtual string RiferimentoAmministrazione
        {
            get
            {
                return riferimentoAmministrazioneField;
            }
            set
            {
                riferimentoAmministrazioneField = value;
            }
        }
    }
}