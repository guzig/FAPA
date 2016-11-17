using FaPA.Core.FaPa;

namespace FaPA.Infrastructure.Dto
{
    public class DatiRitenutaTypeDto : NotDaoBaseEntityDto
    {
        private TipoRitenutaType _tipoRitenutaField;
        private decimal _importoRitenutaField;
        private decimal _aliquotaRitenutaField;
        private CausalePagamentoType _causalePagamentoField;

        public TipoRitenutaType TipoRitenuta
        {
            get
            {
                return _tipoRitenutaField;
            }
            set
            {
                if (value == _tipoRitenutaField) return;
                if (value == _tipoRitenutaField) return;
                _tipoRitenutaField = value;
                
            }
        }

        public decimal ImportoRitenuta
        {
            get
            {
                return _importoRitenutaField;
            }
            set
            {
                if (value == _importoRitenutaField) return;
                _importoRitenutaField = value;
                
            }
        }

        public decimal AliquotaRitenuta
        {
            get
            {
                return _aliquotaRitenutaField;
            }
            set
            {
                if (value == _aliquotaRitenutaField) return;
                _aliquotaRitenutaField = value;
                
            }
        }

        public CausalePagamentoType CausalePagamento
        {
            get
            {
                return _causalePagamentoField;
            }
            set
            {
                if (value == _causalePagamentoField) return;
                _causalePagamentoField = value;
                
            }
        }

        public override bool IsProxy()
        {
            return false;
        }

    }
}
