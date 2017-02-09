using System;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class RappresentanteFiscaleType : BaseEntityFpa
    {

        private DatiAnagraficiRappresentanteType _datiAnagraficiField;
        public virtual  DatiAnagraficiRappresentanteType DatiAnagrafici
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