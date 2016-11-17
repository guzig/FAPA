using System;
using FaPA.Core.FaPa;

namespace FaPA.Infrastructure.Dto
{
    public class DatiPagamentoDto : NotDaoBaseEntityDto
    {

        #region fields

        private string _beneficiarioField;
        private ModalitaPagamentoType _modalitaPagamentoField;
        private DateTime? _dataRiferimentoTerminiPagamentoField;
        private bool _dataRiferimentoTerminiPagamentoFieldSpecified;
        private string _giorniTerminiPagamentoField;
        private DateTime? _dataScadenzaPagamentoField;
        private bool _dataScadenzaPagamentoFieldSpecified;
        private decimal _importoPagamentoField;
        private string _codUfficioPostaleField;
        private string _cognomeQuietanzanteField;
        private string _nomeQuietanzanteField;
        private string _cFQuietanzanteField;
        private string _titoloQuietanzanteField;
        private string _istitutoFinanziarioField;
        private string _iBanField;
        private string _aBiField;
        private string _cAbField;
        private string _bIcField;
        private decimal _scontoPagamentoAnticipatoField;
        private bool _scontoPagamentoAnticipatoFieldSpecified;
        private DateTime? _dataLimitePagamentoAnticipatoField;
        private bool _dataLimitePagamentoAnticipatoFieldSpecified;
        private decimal _penalitaPagamentiRitardatiField;
        private bool _penalitaPagamentiRitardatiFieldSpecified;
        private DateTime? _dataDecorrenzaPenaleField;
        private bool _dataDecorrenzaPenaleFieldSpecified;
        private string _codicePagamentoField;


        #endregion

        private CondizioniPagamentoType _condizioniPagamentoField;

        public CondizioniPagamentoType CondizioniPagamento
        {
            get
            {
                return _condizioniPagamentoField;
            }
            set
            {
                if (value == _condizioniPagamentoField) return;
                _condizioniPagamentoField = value;
                
            }
        }

        public string Beneficiario
        {
            get
            {
                return _beneficiarioField;
            }
            set
            {
                if (value == _beneficiarioField) return;
                _beneficiarioField = value;
                
            }
        }

        public ModalitaPagamentoType ModalitaPagamento
        {
            get
            {
                return _modalitaPagamentoField;
            }
            set
            {
                if (value == _modalitaPagamentoField) return;
                _modalitaPagamentoField = value;
                
            }
        }

        public DateTime? DataRiferimentoTerminiPagamento
        {
            get
            {
                return _dataRiferimentoTerminiPagamentoField;
            }
            set
            {
                if (value.Equals(_dataRiferimentoTerminiPagamentoField)) return;
                _dataRiferimentoTerminiPagamentoField = value;
                
                DataRiferimentoTerminiPagamentoSpecified = _dataRiferimentoTerminiPagamentoField != null;
            }
        }

        public bool DataRiferimentoTerminiPagamentoSpecified
        {
            get
            {
                return _dataRiferimentoTerminiPagamentoFieldSpecified;
            }
            set
            {
                if (value == _dataRiferimentoTerminiPagamentoFieldSpecified) return;
                _dataRiferimentoTerminiPagamentoFieldSpecified = value;
                
            }
        }

        public string GiorniTerminiPagamento
        {
            get
            {
                return _giorniTerminiPagamentoField;
            }
            set
            {
                if (value == _giorniTerminiPagamentoField) return;
                _giorniTerminiPagamentoField = value;
                
            }
        }

        public DateTime? DataScadenzaPagamento
        {
            get
            {
                return _dataScadenzaPagamentoField;
            }
            set
            {
                if (value.Equals(_dataScadenzaPagamentoField)) return;
                _dataScadenzaPagamentoField = value;
                NotifyOfPropertyChange(() => DataScadenzaPagamento);
                

            }
        }

        public bool DataScadenzaPagamentoSpecified
        {
            get
            {
                return _dataScadenzaPagamentoFieldSpecified;
            }
            set
            {
                if (value == _dataScadenzaPagamentoFieldSpecified) return;
                _dataScadenzaPagamentoFieldSpecified = value;
                
            }
        }

        public decimal ImportoPagamento
        {
            get
            {
                return _importoPagamentoField;
            }
            set
            {
                if (value == _importoPagamentoField) return;
                _importoPagamentoField = value;
                

                //RaiseErrorsChanged();
            }
        }

        public string CodUfficioPostale
        {
            get
            {
                return _codUfficioPostaleField;
            }
            set
            {
                if (value == _codUfficioPostaleField) return;
                _codUfficioPostaleField = value;
                
            }
        }

        public string CognomeQuietanzante
        {
            get
            {
                return _cognomeQuietanzanteField;
            }
            set
            {
                if (value == _cognomeQuietanzanteField) return;
                _cognomeQuietanzanteField = value;
                
            }
        }

        public string NomeQuietanzante
        {
            get
            {
                return _nomeQuietanzanteField;
            }
            set
            {
                if (value == _nomeQuietanzanteField) return;
                _nomeQuietanzanteField = value;
                
            }
        }

        public string CfQuietanzante
        {
            get
            {
                return _cFQuietanzanteField;
            }
            set
            {
                if (value == _cFQuietanzanteField) return;
                _cFQuietanzanteField = value;
                
            }
        }

        public string TitoloQuietanzante
        {
            get
            {
                return _titoloQuietanzanteField;
            }
            set
            {
                if (value == _titoloQuietanzanteField) return;
                _titoloQuietanzanteField = value;
                
            }
        }

        public string IstitutoFinanziario
        {
            get
            {
                return _istitutoFinanziarioField;
            }
            set
            {
                if (value == _istitutoFinanziarioField) return;
                _istitutoFinanziarioField = value;
                
            }
        }

        public string Iban
        {
            get
            {
                return _iBanField;
            }
            set
            {
                if (value == _iBanField) return;
                _iBanField = value;
                
            }
        }

        public string Abi
        {
            get
            {
                return _aBiField;
            }
            set
            {
                if (value == _aBiField) return;
                _aBiField = value;
                
            }
        }

        public string Cab
        {
            get
            {
                return _cAbField;
            }
            set
            {
                if (value == _cAbField) return;
                _cAbField = value;
                
            }
        }

        public string Bic
        {
            get
            {
                return _bIcField;
            }
            set
            {
                if (value == _bIcField) return;
                _bIcField = value;
                
            }
        }

        public decimal ScontoPagamentoAnticipato
        {
            get
            {
                return _scontoPagamentoAnticipatoField;
            }
            set
            {
                if (value == _scontoPagamentoAnticipatoField) return;
                _scontoPagamentoAnticipatoField = value;
                

                ScontoPagamentoAnticipatoSpecified = _scontoPagamentoAnticipatoField > decimal.MinValue;
            }
        }

        public bool ScontoPagamentoAnticipatoSpecified
        {
            get
            {
                return _scontoPagamentoAnticipatoFieldSpecified;
            }
            set
            {
                if (value == _scontoPagamentoAnticipatoFieldSpecified) return;
                _scontoPagamentoAnticipatoFieldSpecified = value;
                
            }
        }

        public DateTime? DataLimitePagamentoAnticipato
        {
            get
            {
                return _dataLimitePagamentoAnticipatoField;
            }
            set
            {
                if (value.Equals(_dataLimitePagamentoAnticipatoField)) return;
                _dataLimitePagamentoAnticipatoField = value;
                
                DataLimitePagamentoAnticipatoSpecified = _dataLimitePagamentoAnticipatoField != null;
            }
        }

        public bool DataLimitePagamentoAnticipatoSpecified
        {
            get
            {
                return _dataLimitePagamentoAnticipatoFieldSpecified;
            }
            set
            {
                if (value == _dataLimitePagamentoAnticipatoFieldSpecified) return;
                _dataLimitePagamentoAnticipatoFieldSpecified = value;
                
            }
        }

        public decimal PenalitaPagamentiRitardati
        {
            get
            {
                return _penalitaPagamentiRitardatiField;
            }
            set
            {
                if (value == _penalitaPagamentiRitardatiField) return;
                _penalitaPagamentiRitardatiField = value;
                

                PenalitaPagamentiRitardatiSpecified = _penalitaPagamentiRitardatiField > decimal.MinValue;
            }
        }

        public bool PenalitaPagamentiRitardatiSpecified
        {
            get
            {
                return _penalitaPagamentiRitardatiFieldSpecified;
            }
            set
            {
                if (value == _penalitaPagamentiRitardatiFieldSpecified) return;
                _penalitaPagamentiRitardatiFieldSpecified = value;
                
            }
        }

        public DateTime? DataDecorrenzaPenale
        {
            get
            {
                return _dataDecorrenzaPenaleField;
            }
            set
            {
                if (value.Equals(_dataDecorrenzaPenaleField)) return;
                _dataDecorrenzaPenaleField = value;
                
                DataDecorrenzaPenaleSpecified = _dataDecorrenzaPenaleField != null;
            }
        }

        public bool DataDecorrenzaPenaleSpecified
        {
            get
            {
                return _dataDecorrenzaPenaleFieldSpecified;
            }
            set
            {
                if (value == _dataDecorrenzaPenaleFieldSpecified) return;
                _dataDecorrenzaPenaleFieldSpecified = value;
                
            }
        }

        public string CodicePagamento
        {
            get
            {
                return _codicePagamentoField;
            }
            set
            {
                if (value == _codicePagamentoField) return;
                _codicePagamentoField = value;
                
            }
        }

        public override bool IsProxy()
        {
            return false;
        }

        //public override System.Collections.IEnumerable GetErrors(string propertyName)
        //{
        //   if (DomainResult == null || DomainResult.Success)
        //        return null;
        //    return DomainResult.PropErrors(propertyName);
        //}

        //public override bool HasErrors => DomainResult != null && !DomainResult.Success;

    }
}