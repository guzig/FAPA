using System;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class TerzoIntermediarioSoggettoEmittenteType: BaseEntityFpa
    {

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