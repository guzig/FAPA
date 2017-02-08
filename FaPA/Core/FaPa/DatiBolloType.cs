using System;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DatiBolloType : BaseEntityFpa
    {
        private BolloVirtualeType _bolloVirtualeField;
        private decimal _importoBolloField;
        
        public virtual BolloVirtualeType BolloVirtuale
        {
            get
            {
                return _bolloVirtualeField;
            }
            set
            {
                _bolloVirtualeField = value;
            }
        }
        
        public virtual decimal ImportoBollo
        {
            get
            {
                return _importoBolloField;
            }
            set
            {
                _importoBolloField = value;
            }
        }
    }
}