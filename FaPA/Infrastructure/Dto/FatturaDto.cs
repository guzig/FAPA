using System;
using System.Collections.Generic;
using FaPA.Core;
using FaPA.Core.FaPa;

namespace FaPA.Infrastructure.Dto
{
    public class FatturaDto : BaseEntityDto
    {
        #region fields

        private FatturaElettronicaType _fatturaPa;

        //rtitenuta
        private CausalePagamentoType _causalePagamentoField;
        private Fornitore _anagraficaCedente;
        private Committente _anagraficaCommittente;
        private DateTime _dataFatturaDb = DateTime.Now;       
        private decimal _totaleFatturaDb;
        private string _numeroFattura;
        private string _cigDb;
        private string _cupDb;
        private string _codUfficioDb;
        private string _noteDb;
        private DatiRitenutaType _ritenuta;
        private DatiCassaPrevidenzialeType _cassaPrevidenziale;
        private bool _cassaPrevidenzialeSpecified;
        private string _codiceCommessaConvenzioneDb;
        private string _idDocumentoDb;

        #endregion

        #region props

        //fatturapa members
        public FatturaElettronicaType FatturaPa
        {
            get { return _fatturaPa; }
            set
            {
                if (Equals(value, _fatturaPa)) return;
                _fatturaPa = value;
                NotifyOfPropertyChange();
            }
        }

        //public DatiGeneraliDocumentoType DatiGeneraliDocumento
        //{
        //    get
        //    {
        //        if (FatturaPa.FatturaElettronicaBody == null )
        //        {
        //            FatturaPa.FatturaElettronicaBody = new FatturaElettronicaBodyType();
        //        }
        //        if ( FatturaPa.FatturaElettronicaBody.DatiGenerali == null )
        //        {
        //            FatturaPa.FatturaElettronicaBody.DatiGenerali = new DatiGeneraliType();
        //        }

        //        if ( FatturaPa.FatturaElettronicaBody.DatiGenerali.DatiGeneraliDocumento == null )
        //        {
        //            FatturaPa.FatturaElettronicaBody.DatiGenerali.DatiGeneraliDocumento = new DatiGeneraliDocumentoType();
        //        }

        //        return FatturaPa.FatturaElettronicaBody.DatiGenerali.DatiGeneraliDocumento;
        //    }
        //}

        public DatiBeniServiziType DatiBeniServizi
        {
            get
            {
                if (FatturaPa.FatturaElettronicaBody == null )
                {
                    FatturaPa.FatturaElettronicaBody = new FatturaElettronicaBodyType();
                }

                if ( FatturaPa.FatturaElettronicaBody.DatiBeniServizi == null)
                {
                    FatturaPa.FatturaElettronicaBody.DatiBeniServizi = new DatiBeniServiziType();
                }

                return FatturaPa.FatturaElettronicaBody.DatiBeniServizi;
            }
        }

        public IList<DettaglioLineeType> DettaglioLinee => DatiBeniServizi.DettaglioLinee;

        public DatiRitenutaType Ritenuta
        {
            get { return _ritenuta; }
            set
            {
                if (Equals(value, _ritenuta)) return;
                _ritenuta = value;
                NotifyOfPropertyChange(() => Ritenuta);
            }
        }

        private DatiPagamentoType[] _datiPagamento;
        public DatiPagamentoType[] DatiPagamento
        {
            get { return _datiPagamento; }
            set
            {
                if (Equals(value, _datiPagamento)) return;
                _datiPagamento = value;
                NotifyOfPropertyChange(() => DatiPagamento);
            }
        }
      

        public DatiCassaPrevidenzialeType CassaPrevidenziale
        {
            get { return _cassaPrevidenziale; }
            set
            {
                if (Equals(value, _cassaPrevidenziale)) return;
                _cassaPrevidenziale = value;
                NotifyOfPropertyChange(() => CassaPrevidenziale);
            }
        }

        public CedentePrestatoreType CedenteFornitore
        {
            get
            {
                if (FatturaPa.FatturaElettronicaHeader == null)
                {
                    FatturaPa.FatturaElettronicaHeader = new FatturaElettronicaHeaderType();
                }

                return FatturaPa.FatturaElettronicaHeader.CedentePrestatore ??
                       (FatturaPa.FatturaElettronicaHeader.CedentePrestatore = new CedentePrestatoreType());
            }
        }

        public virtual AnagraficaType AnagraficaCedenteFornitore
        {
            get
            {
                if ( CedenteFornitore.DatiAnagrafici == null )
                {
                    CedenteFornitore.DatiAnagrafici = new DatiAnagraficiCedenteType();
                    CedenteFornitore.DatiAnagrafici.Anagrafica = new AnagraficaType();
                    return CedenteFornitore.DatiAnagrafici.Anagrafica;
                }
                if ( CedenteFornitore.DatiAnagrafici.Anagrafica == null )
                {
                    CedenteFornitore.DatiAnagrafici.Anagrafica = new AnagraficaType();
                    return CedenteFornitore.DatiAnagrafici.Anagrafica;
                }
                return CedenteFornitore.DatiAnagrafici.Anagrafica;
            }
        }

        public RegimeFiscaleType RegimeFiscale
        {
            get { return CedenteFornitore.DatiAnagrafici.RegimeFiscale; }
            set
            {
                CedenteFornitore.DatiAnagrafici.RegimeFiscale = value;
                NotifyOfPropertyChange( () => RegimeFiscale );
            }
        }

        //committente
        public CessionarioCommittenteType CessionarioCommittente
        {
            get
            {
                if (FatturaPa.FatturaElettronicaHeader == null)
                {
                    FatturaPa.FatturaElettronicaHeader = new FatturaElettronicaHeaderType();
                }

                return FatturaPa.FatturaElettronicaHeader.CessionarioCommittente ??
                       (FatturaPa.FatturaElettronicaHeader.CessionarioCommittente = new CessionarioCommittenteType());
            }
        }

        public virtual AnagraficaType AnagraficaCessionarioCommittente
        {
            get
            {
                if ( CessionarioCommittente.DatiAnagrafici == null )
                {
                    CessionarioCommittente.DatiAnagrafici = new DatiAnagraficiCessionarioType();
                    CessionarioCommittente.DatiAnagrafici.Anagrafica = new AnagraficaType();
                    return CessionarioCommittente.DatiAnagrafici.Anagrafica;
                }

                if ( CessionarioCommittente.DatiAnagrafici.Anagrafica == null )
                {
                    CessionarioCommittente.DatiAnagrafici.Anagrafica = new AnagraficaType();
                    return CessionarioCommittente.DatiAnagrafici.Anagrafica;
                }
                return CessionarioCommittente.DatiAnagrafici.Anagrafica;
            }
        }

        //dati a db
        public virtual DateTime DataCaricamentoDB { get; protected set; }

        public virtual string CigDB
        {
            get { return _cigDb; }
            set
            {
                if (value == _cigDb) return;
                _cigDb = value;
                RaiseNotifyAndErrorsChanged();
            }
        }

        public virtual string CupDB
        {
            get { return _cupDb; }
            set
            {
                if (value == _cupDb) return;
                _cupDb = value;
                RaiseNotifyAndErrorsChanged();
            }
        }

        public virtual string CodUfficioDB
        {
            get { return _codUfficioDb; }
            set
            {
                if (value == _codUfficioDb) return;
                _codUfficioDb = value;
                RaiseNotifyAndErrorsChanged();
            }
        }

        public string IdDocumentoDB
        {
            get { return _idDocumentoDb; }
            set
            {
                if ( value == _idDocumentoDb ) return;
                _idDocumentoDb = value;
                NotifyOfPropertyChange( () => IdDocumentoDB );
            }
        }

        public virtual string NoteDB
        {
            get { return _noteDb; }
            set
            {
                if (value == _noteDb) return;
                _noteDb = value;
                NotifyOfPropertyChange(() => NoteDB);
            }
        }

        public virtual Fornitore AnagraficaCedenteDB
        {
            get { return _anagraficaCedente; }
            set
            {
                if (Equals(value, _anagraficaCedente)) return;
                _anagraficaCedente = value;
                RaiseNotifyAndErrorsChanged();
                RaiseErrorsChanged(() => NumeroFatturaDB);
            }
        }

        public virtual Committente AnagraficaCommittenteDB
        {
            get { return _anagraficaCommittente; }
            set
            {
                if (Equals(value, _anagraficaCommittente)) return;
                _anagraficaCommittente = value;
                RaiseNotifyAndErrorsChanged();
                RaiseErrorsChanged(() => NumeroFatturaDB);
            }
        }

        public virtual string NumeroFatturaDB
        {
            get { return _numeroFattura; }
            set
            {
                if (value == _numeroFattura) return;
                _numeroFattura = value;
                RaiseNotifyAndErrorsChanged();
            }
        }

        public virtual DateTime DataFatturaDB
        {
            get { return _dataFatturaDb; }
            set
            {
                if (value.Equals(_dataFatturaDb)) return;
                _dataFatturaDb = value;
                RaiseNotifyAndErrorsChanged();
                RaiseErrorsChanged(() => NumeroFatturaDB);
            }
        }

        public virtual decimal TotaleFatturaDB
        {
            get { return _totaleFatturaDb; }
            set
            {
                if ( value.Equals( _totaleFatturaDb ) ) return;
                _totaleFatturaDb = value;
                RaiseNotifyAndErrorsChanged();
            }
        }
     
        #endregion

        public void Init()
        {
            FatturaPa = new FatturaElettronicaType();
            //object getLazyInstnce = DatiGeneraliDocumento;
            //getLazyInstnce = fattura.DatiRiepilogo;
            object getLazyInstnce = DettaglioLinee;
            getLazyInstnce = AnagraficaCedenteFornitore;
            getLazyInstnce = AnagraficaCessionarioCommittente;
            Ritenuta = new DatiRitenutaType();
        }

        public override bool IsProxy()
        {
            var forceToLoad = NumeroFatturaDB;
            return Id == 0;
        }

        //public override string Error
        //{
        //    get
        //    {
        //        //var arg = new ValidationEventArgs(null);
        //        //OnDataErrorInfoChanged(arg);
        //        //return arg.Result;
        //        return null;
        //    }
        //}

        //public override string this[string columnName]
        //{
        //    get
        //    {
        //        var arg = new ValidationEventArgs(columnName);
        //        OnDataErrorInfoChanged(arg);
        //        return arg.Result;
        //    }
        //}

    }
}