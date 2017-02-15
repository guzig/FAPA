using System;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DettaglioLineeType : BaseEntityFpa
    {

        #region fields

        private string _numeroLineaField;

        private TipoCessionePrestazioneType _tipoCessionePrestazioneField;

        private bool _tipoCessionePrestazioneFieldSpecified;

        private CodiceArticoloType[] _codiceArticoloField;

        private string _descrizioneField;

        private decimal _quantitaField;

        private bool _quantitaFieldSpecified;

        private string _unitaMisuraField;

        private DateTime _dataInizioPeriodoField;

        private bool _dataInizioPeriodoFieldSpecified;

        private DateTime _dataFinePeriodoField;

        private bool _dataFinePeriodoFieldSpecified;

        private decimal _prezzoUnitarioField;

        private ScontoMaggiorazioneType[] _scontoMaggiorazioneField;

        private decimal _prezzoTotaleField;

        private decimal _aliquotaIvaField;

        private RitenutaType _ritenutaField;

        private bool _ritenutaFieldSpecified;

        private NaturaType _naturaField;

        private bool _naturaFieldSpecified;

        private string _riferimentoAmministrazioneField;

        private AltriDatiGestionaliType[] _altriDatiGestionaliField;
        
        #endregion
        
        public virtual  string NumeroLinea
        {
            get
            {
                return _numeroLineaField;
            }
            set
            {
                _numeroLineaField = value;
            }
        }


        public virtual TipoCessionePrestazioneType TipoCessionePrestazione
        {
            get
            {
                return _tipoCessionePrestazioneField;
            }
            set
            {
                _tipoCessionePrestazioneField = value;
                TipoCessionePrestazioneSpecified = _tipoCessionePrestazioneField != TipoCessionePrestazioneType.N;
            }
        }

        [XmlIgnore]
        public virtual bool TipoCessionePrestazioneSpecified
        {
            get
            {
                return _tipoCessionePrestazioneFieldSpecified;
            }
            set
            {
                _tipoCessionePrestazioneFieldSpecified = value;
            }
        }

        [XmlIgnore]
        public virtual CodiceArticoloType[] CodiceArticolo
        {
            get
            {
                return _codiceArticoloField;
            }
            set
            {
                _codiceArticoloField = value;
            }
        }

        public virtual string Descrizione
        {
            get
            {
                return _descrizioneField;
            }
            set
            {
                _descrizioneField = value;
            }
        }

        public virtual decimal Quantita
        {
            get
            {
                return _quantitaField;
            }
            set
            {
                _quantitaField = decimal.Parse(string.Format("{0:###0.00}",value) );
                QuantitaSpecified = _quantitaField > 0 || _quantitaField < 0;
            }
        }

        [XmlIgnore]
        public bool QuantitaSpecified
        {
            get
            {
                return _quantitaFieldSpecified;
            }
            set
            {
                _quantitaFieldSpecified = value;
            }
        }

        public virtual string UnitaMisura
        {
            get
            {
                return _unitaMisuraField;
            }
            set
            {
                _unitaMisuraField = value;
            }
        }

        public virtual DateTime DataInizioPeriodo
        {
            get
            {
                return _dataInizioPeriodoField;
            }
            set
            {
                _dataInizioPeriodoField = value;
                DataInizioPeriodoSpecified = _dataInizioPeriodoField != DateTime.MinValue;
            }
        }

        [XmlIgnore]
        public virtual bool DataInizioPeriodoSpecified
        {
            get
            {
                return _dataInizioPeriodoFieldSpecified;
            }
            set
            {
                _dataInizioPeriodoFieldSpecified = value;
            }
        }

        public virtual DateTime DataFinePeriodo
        {
            get
            {
                return _dataFinePeriodoField;
            }
            set
            {
                _dataFinePeriodoField = value;
                DataFinePeriodoSpecified = _dataFinePeriodoField != DateTime.MinValue && _dataFinePeriodoField != DateTime.MaxValue;
            }
        }

        [XmlIgnore]
        public virtual bool DataFinePeriodoSpecified
        {
            get
            {
                return _dataFinePeriodoFieldSpecified;
            }
            set
            {
                _dataFinePeriodoFieldSpecified = value;
            }
        }

        public virtual decimal PrezzoUnitario
        {
            get
            {
                return _prezzoUnitarioField;
            }
            set
            {
                _prezzoUnitarioField = decimal.Parse(string.Format("{0:###0.00}", value));
            }
        }

        [XmlElement]
        public virtual ScontoMaggiorazioneType[] ScontoMaggiorazione
        {
            get
            {
                return _scontoMaggiorazioneField;
            }
            set
            {
                _scontoMaggiorazioneField = value;
            }
        }

        public virtual decimal PrezzoTotale
        {
            get
            {
                return _prezzoTotaleField;
            }
            set
            {
                _prezzoTotaleField = decimal.Parse(string.Format("{0:###0.00}", value));
            }
        }

        public virtual decimal AliquotaIVA
        {
            get
            {
                return _aliquotaIvaField;
            }
            set
            {
                _aliquotaIvaField = decimal.Parse(string.Format("{0:###0.00}", value));
            }
        }

        public virtual RitenutaType Ritenuta
        {
            get
            {
                return _ritenutaField;
            }
            set
            {
                _ritenutaField = value;
                RitenutaSpecified = _ritenutaField == RitenutaType.SI;
            }
        }

        [XmlIgnore]
        public virtual bool RitenutaSpecified
        {
            get
            {
                return _ritenutaFieldSpecified;
            }
            set
            {
                _ritenutaFieldSpecified = value;
            }
        }

        public virtual NaturaType Natura
        {
            get
            {
                return _naturaField;
            }
            set
            {
                _naturaField = value;
                NaturaSpecified = _naturaField != NaturaType.N;
            }
        }

        [XmlIgnore]
        public virtual bool NaturaSpecified
        {
            get
            {
                return _naturaFieldSpecified;
            }
            set
            {
                _naturaFieldSpecified = value;
            }
        }

        public virtual string RiferimentoAmministrazione
        {
            get
            {
                return _riferimentoAmministrazioneField;
            }
            set
            {
                _riferimentoAmministrazioneField = value;
            }
        }

        [XmlElement]
        public virtual AltriDatiGestionaliType[] AltriDatiGestionali
        {
            get
            {
                return _altriDatiGestionaliField;
            }
            set
            {
                _altriDatiGestionaliField = value;
            }
        }
    }
}