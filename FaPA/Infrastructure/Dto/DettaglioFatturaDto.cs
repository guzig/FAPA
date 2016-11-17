using System;
using System.Linq;
using FaPA.AppServices.CoreValidation;
using FaPA.Core.FaPa;

namespace FaPA.Infrastructure.Dto
{
    public class DettaglioFatturaDto : NotDaoBaseEntityDto
    {
        #region fields

        private string _numeroLineaField;
        private TipoCessionePrestazioneType _tipoCessionePrestazioneField;
        private bool _tipoCessionePrestazioneFieldSpecified;
        //private CodiceArticoloType[] _codiceArticoloField;
        private string _descrizioneField;
        private decimal _quantitaField;
        private bool _quantitaFieldSpecified;
        private string _unitaMisuraField;
        private DateTime _dataInizioPeriodoField;
        private bool _dataInizioPeriodoFieldSpecified;
        private DateTime _dataFinePeriodoField;
        private bool _dataFinePeriodoFieldSpecified;
        private decimal _prezzoUnitarioField;
        //private ScontoMaggiorazioneType[] _scontoMaggiorazioneField;
        private decimal _prezzoTotaleField;
        private decimal _aliquotaIvaField;
        private RitenutaType _ritenutaField;
        private bool _ritenutaFieldSpecified;
        private NaturaType _naturaField;
        private bool _naturaFieldSpecified;
        private string _riferimentoAmministrazioneField;
        private AltriDatiGestionaliType[] _altriDatiGestionaliField ;

        #endregion

        public string NumeroLinea
        {
            get
            {
                return _numeroLineaField;
            }
            set
            {
                if ( value == _numeroLineaField ) return;
                _numeroLineaField = value;
                
            }
        }

        public TipoCessionePrestazioneType TipoCessionePrestazione
        {
            get
            {
                return _tipoCessionePrestazioneField;
            }
            set
            {
                if ( value == _tipoCessionePrestazioneField ) return;
                _tipoCessionePrestazioneField = value;
                
            }
        }

        public bool TipoCessionePrestazioneSpecified
        {
            get
            {
                return _tipoCessionePrestazioneFieldSpecified;
            }
            set
            {
                if ( value == _tipoCessionePrestazioneFieldSpecified ) return;
                _tipoCessionePrestazioneFieldSpecified = value;
                
            }
        }

        //public CodiceArticoloType[] CodiceArticolo
        //{
        //    get
        //    {
        //        return _codiceArticoloField;
        //    }
        //    set
        //    {
        //        if ( Equals( value, _codiceArticoloField ) ) return;
        //        _codiceArticoloField = value;
        //        NotifyOfPropertyChange( () => CodiceArticolo );
        //    }
        //}

        public string Descrizione
        {
            get
            {
                return _descrizioneField;
            }
            set
            {
                if ( value == _descrizioneField ) return;
                _descrizioneField = value;
                
            }
        }

        public decimal Quantita
        {
            get
            {
                return _quantitaField;
            }
            set
            {
                if ( value == _quantitaField ) return;
                _quantitaField = value;
                
            }
        }

        public bool QuantitaSpecified
        {
            get
            {
                return _quantitaFieldSpecified;
            }
            set
            {
                if ( value == _quantitaFieldSpecified ) return;
                _quantitaFieldSpecified = value;
                
            }
        }

        public string UnitaMisura
        {
            get
            {
                return _unitaMisuraField;
            }
            set
            {
                if ( value == _unitaMisuraField ) return;
                _unitaMisuraField = value;
                
            }
        }

        public DateTime DataInizioPeriodo
        {
            get
            {
                return _dataInizioPeriodoField;
            }
            set
            {
                if ( value.Equals( _dataInizioPeriodoField ) ) return;
                _dataInizioPeriodoField = value;
                
            }
        }

        public bool DataInizioPeriodoSpecified
        {
            get
            {
                return _dataInizioPeriodoFieldSpecified;
            }
            set
            {
                if ( value == _dataInizioPeriodoFieldSpecified ) return;
                _dataInizioPeriodoFieldSpecified = value;
                
            }
        }

        public DateTime DataFinePeriodo
        {
            get
            {
                return _dataFinePeriodoField;
            }
            set
            {
                if ( value.Equals( _dataFinePeriodoField ) ) return;
                _dataFinePeriodoField = value;
                
            }
        }

        public bool DataFinePeriodoSpecified
        {
            get
            {
                return _dataFinePeriodoFieldSpecified;
            }
            set
            {
                if ( value == _dataFinePeriodoFieldSpecified ) return;
                _dataFinePeriodoFieldSpecified = value;
                
            }
        }

        public decimal PrezzoUnitario
        {
            get
            {
                return _prezzoUnitarioField;
            }
            set
            {
                if ( value == _prezzoUnitarioField ) return;
                _prezzoUnitarioField = value;
                
            }
        }

        //public ScontoMaggiorazioneType[] ScontoMaggiorazione
        //{
        //    get
        //    {
        //        return _scontoMaggiorazioneField;
        //    }
        //    set
        //    {
        //        if ( Equals( value, _scontoMaggiorazioneField ) ) return;
        //        _scontoMaggiorazioneField = value;
        //        NotifyOfPropertyChange( () => ScontoMaggiorazione );
        //    }
        //}

        public decimal PrezzoTotale
        {
            get
            {
                return _prezzoTotaleField;
            }
            set
            {
                if ( value == _prezzoTotaleField ) return;
                _prezzoTotaleField = value;
                
            }
        }

        public decimal AliquotaIVA
        {
            get
            {
                return _aliquotaIvaField;
            }
            set
            {
                if ( value == _aliquotaIvaField ) return;
                _aliquotaIvaField = value;
                
            }
        }

        public RitenutaType Ritenuta
        {
            get
            {
                return _ritenutaField;
            }
            set
            {
                if ( value == _ritenutaField ) return;
                _ritenutaField = value;
                
            }
        }

        public bool RitenutaSpecified
        {
            get
            {
                return _ritenutaFieldSpecified;
            }
            set
            {
                if ( value == _ritenutaFieldSpecified ) return;
                _ritenutaFieldSpecified = value;
                
            }
        }

        public NaturaType Natura
        {
            get
            {
                return _naturaField;
            }
            set
            {
                if ( value == _naturaField ) return;
                _naturaField = value;
                
            }
        }

        public bool NaturaSpecified
        {
            get
            {
                return _naturaFieldSpecified;
            }
            set
            {
                if ( value == _naturaFieldSpecified ) return;
                _naturaFieldSpecified = value;
                
            }
        }

        public string RiferimentoAmministrazione
        {
            get
            {
                return _riferimentoAmministrazioneField;
            }
            set
            {
                if ( value == _riferimentoAmministrazioneField ) return;
                _riferimentoAmministrazioneField = value;
                
            }
        }

        public AltriDatiGestionaliType[] AltriDatiGestionali
        {
            get
            {
                return _altriDatiGestionaliField;
            }
            set
            {
                //this is a trick cause of bug in datagrid comboxtemplate  
                _altriDatiGestionaliField = value;
                _isAltriDati = true;
                
                _isAltriDati = false;
            }
        }
        
        private bool _isAltriDati;

        public override bool IsProxy()
        {
            return false;
        }

        //public override string Error
        //{
        //    get
        //    {
        //        if (AltriDatiGestionali == null)
        //            return null;

        //        foreach ( var item in AltriDatiGestionali )
        //        {
        //           var error = CoreValidatorService.GetValidationErrors( item )?.FirstOrDefault();
        //            if ( error != null && error.Value.Value.Any(s=>!string.IsNullOrWhiteSpace( s )) )
        //            {
        //                return error.Value.Key + ": " + error.Value.Value.FirstOrDefault( s => !string.IsNullOrWhiteSpace( s ) );
        //            }
        //        }
        //        return null;

        //    }

        //}

        public string GetError
        {
            get
            {
                if (AltriDatiGestionali == null)
                    return null;

                foreach (var item in AltriDatiGestionali)
                {
                    var error = CoreValidatorService.GetValidationErrors(item)?.FirstOrDefault();
                    if (error != null && error.Value.Value.Any(s => !string.IsNullOrWhiteSpace(s)))
                    {
                        return error.Value.Key + ": " + error.Value.Value.FirstOrDefault(s => !string.IsNullOrWhiteSpace(s));
                    }
                }
                return null;

            }

        }

        //public override System.Collections.IEnumerable GetErrors(string propertyName)
        //{
        //    if ( _isAltriDati)
        //    {
        //            var s = GetError;
        //            if (string.IsNullOrWhiteSpace(s)) return null;
        //            return new[] { "dddd" };               
        //    }
        //    if (string.IsNullOrEmpty(propertyName))
        //        return null;

        //    if (propertyName == "AltriDatiGestionali")
        //        if ( AltriDatiGestionali == null )
        //            return new[] {"dddd"};

        //    return null;
        //}

        //public override bool HasErrors
        //{
        //    get
        //    {
        //        if (_isAltriDati)
        //        {
        //            var s = GetError;
        //            if (string.IsNullOrWhiteSpace(s)) return false;
        //            return true;
        //        }

        //        return false;
        //    }
        //}

    }
}
