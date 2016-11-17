using System;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DatiRitenutaType : BaseEntityFpa
    {
        private TipoRitenutaType _tipoRitenutaField;
        private decimal _importoRitenutaField;
        private decimal _aliquotaRitenutaField;
        private CausalePagamentoType _causalePagamentoField;

        public virtual TipoRitenutaType TipoRitenuta
        {
            get
            {
                return _tipoRitenutaField;
            }
            set
            {
                if (Equals( value, _tipoRitenutaField )) return;
                _tipoRitenutaField = value;
            }
        }

        public virtual decimal ImportoRitenuta
        {
            get
            {
                return _importoRitenutaField;
            }
            set
            {
                if (value == _importoRitenutaField) return;
                _importoRitenutaField = decimal.Parse(string.Format("{0:###0.00}", value)); 
            }
        }

        public virtual decimal AliquotaRitenuta
        {
            get
            {
                return _aliquotaRitenutaField;
            }
            set
            {
                if (value == _aliquotaRitenutaField) return;
                _aliquotaRitenutaField = decimal.Parse(string.Format("{0:###0.00}", value)); 
            }
        }

        public virtual CausalePagamentoType CausalePagamento
        {
            get
            {
                return _causalePagamentoField;
            }
            set
            {
                if (Equals( value, _causalePagamentoField )) return;
                _causalePagamentoField = value;
            }
        }
    }
}