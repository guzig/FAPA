using System;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class TerzoIntermediarioSoggettoEmittenteType: BaseEntityFpa
    {
        private SoggettoEmittenteType _soggettoEmittente;
        public SoggettoEmittenteType SoggettoEmittente
        {
            get { return _soggettoEmittente; }
            set
            {
                if ( value == _soggettoEmittente ) return;
                _soggettoEmittente = value;
            }
        }

        private DatiAnagraficiTerzoIntermediarioType datiAnagraficiField;

        public virtual DatiAnagraficiTerzoIntermediarioType DatiAnagrafici
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
    }
}