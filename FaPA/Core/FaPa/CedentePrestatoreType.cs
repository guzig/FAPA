using System;

namespace FaPA.Core.FaPa
{
    [Serializable]
    
    public class CedentePrestatoreType : BaseEntityFpa
    {
        #region fields

        private DatiAnagraficiCedenteType _datiAnagraficiField;

        private IndirizzoType _sedeField;

        private IndirizzoType _stabileOrganizzazioneField;

        private IscrizioneREAType _iscrizioneReaField;

        private ContattiType _contattiField;

        private string _riferimentoAmministrazioneField;

        #endregion

        public virtual DatiAnagraficiCedenteType DatiAnagrafici
        {
            get
            {
                return _datiAnagraficiField;
            }
            set
            {
                _datiAnagraficiField = value;
            }
        }

        public virtual IndirizzoType Sede
        {
            get
            {
                return _sedeField;
            }
            set
            {
                _sedeField = value;
            }
        }

        public virtual IndirizzoType StabileOrganizzazione
        {
            get
            {
                return _stabileOrganizzazioneField;
            }
            set
            {
                _stabileOrganizzazioneField = value;
            }
        }

        public virtual IscrizioneREAType IscrizioneREA
        {
            get
            {
                return _iscrizioneReaField;
            }
            set
            {
                _iscrizioneReaField = value;
            }
        }

        public virtual ContattiType Contatti
        {
            get
            {
                return _contattiField;
            }
            set
            {
                _contattiField = value;
            }
        }

        public virtual string RiferimentoAmministrazione
        {
            get
            {
                return _riferimentoAmministrazioneField;
            }
            set
            {
                _riferimentoAmministrazioneField = value;
            }
        }
    }
}