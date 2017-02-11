using System;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class TerzoIntermediarioSoggettoEmittenteType: BaseEntityFpa
    {
        private SoggettoEmittenteType _soggettoEmittente;

        [XmlIgnore]
        public SoggettoEmittenteType SoggettoEmittente
        {
            get { return _soggettoEmittente; }
            set
            {
                if ( value == _soggettoEmittente ) return;
                _soggettoEmittente = value;
            }
        }

        private DatiAnagraficiTerzoIntermediarioType _datiAnagraficiField;

        public virtual DatiAnagraficiTerzoIntermediarioType DatiAnagrafici
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
    }
}