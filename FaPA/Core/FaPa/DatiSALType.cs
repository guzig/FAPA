using System;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DatiSALType : BaseEntityFpa
    {
        private string _riferimentoFaseField;
        
        public virtual string RiferimentoFase
        {
            get
            {
                return _riferimentoFaseField;
            }
            set
            {
                _riferimentoFaseField = value;
            }
        }
    }
}