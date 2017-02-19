using System;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class CessionarioCommittenteType: BaseEntityFpa 
    {
        private DatiAnagraficiCessionarioType _datiAnagraficiField;
        private IndirizzoType _stabileOrganizzazioneField;
        private IndirizzoType _sedeField;
        private RappresentanteFiscaleCessionarioType _rappresentanteFiscaleCessionarioTypeField;

        public virtual DatiAnagraficiCessionarioType DatiAnagrafici
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

        /// <summary>
        /// può essere valorizzato solo se la fattura è destinata ad un privato (FormatoTrasmissione = ‘FPR12’).
        /// </summary>
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

        /// <summary>
        /// può essere valorizzato solo se la fattura è destinata ad un privato (FormatoTrasmissione = ‘FPR12’)
        /// </summary>
        public virtual RappresentanteFiscaleCessionarioType RappresentanteFiscaleFiscale
        {
            get
            {
                return _rappresentanteFiscaleCessionarioTypeField;
            }
            set
            {
                _rappresentanteFiscaleCessionarioTypeField = value;
            }
        }
    }
}