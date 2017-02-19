using System;
using System.Globalization;
using System.Xml.Schema;
using System.Xml.Serialization;
using FaPA.DomainServices.Utils;

namespace FaPA.Core.FaPa
{
    [Serializable]
    
    public class DatiGeneraliDocumentoType : BaseEntityFpa
    {

        #region

        private TipoDocumentoType _tipoDocumentoField;

        private string _divisaField;

        private DateTime _dataField;

        private string _numeroField;

        private DatiRitenutaType _datiRitenutaField;

        private DatiBolloType _datiBolloField;

        private DatiCassaPrevidenzialeType _datiCassaPrevidenzialeField;

        private ScontoMaggiorazioneType[] _scontoMaggiorazioneField;

        private decimal _importoTotaleDocumentoField;

        private bool _importoTotaleDocumentoFieldSpecified;

        private decimal _arrotondamentoField;

        private bool _arrotondamentoFieldSpecified;

        private string[] _causaleField;

        private Art73Type _art73Field;

        private bool _art73FieldSpecified;


        #endregion

        public virtual TipoDocumentoType TipoDocumento
        {
            get
            {
                return _tipoDocumentoField;
            }
            set
            {
                _tipoDocumentoField = value;
            }
        }

        public virtual string Divisa
        {
            get
            {
                return _divisaField;
            }
            set
            {
                _divisaField = value;
            }
        }

        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "date" )]
        public virtual DateTime Data
        {
            get
            {
                return _dataField;
            }
            set
            {
                _dataField = value;
            }
        }

        public virtual string Numero
        {
            get
            {
                return _numeroField;
            }
            set
            {
                _numeroField = value;
            }
        }


        
        public virtual DatiRitenutaType DatiRitenuta
        {
            get
            {
                return _datiRitenutaField;
            }
            set
            {
                _datiRitenutaField = value;
            }
        }

        public virtual DatiBolloType DatiBollo
        {
            get
            {
                return _datiBolloField;
            }
            set
            {
                _datiBolloField = value;
            }
        }

        //[XmlIgnore]
        //public virtual bool DatiCassaPrevidenzialeSpecified { get; set; }

        public virtual DatiCassaPrevidenzialeType DatiCassaPrevidenziale
        {
            get
            {
                return _datiCassaPrevidenzialeField;
            }
            set
            {
                _datiCassaPrevidenzialeField = value;
                //DatiCassaPrevidenzialeSpecified = _datiCassaPrevidenzialeField.ImportoContributoCassa != 0;
            }
        }

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

        public virtual decimal ImportoTotaleDocumento
        {
            get
            {
                return _importoTotaleDocumentoField;
                //_importoTotaleDocumentoField.ToString("0.000",CultureInfo.InvariantCulture);
            }
            set
            {
                //_importoTotaleDocumentoField = value.ToCustomFormatDecimal();
                _importoTotaleDocumentoField = value;
                ImportoTotaleDocumentoSpecified = _importoTotaleDocumentoField > (decimal) 0.0 ||
                    _importoTotaleDocumentoField < (decimal) 0.0;
            }
        }

        [XmlIgnore]
        public bool ImportoTotaleDocumentoSpecified
        {
            get
            {
                return _importoTotaleDocumentoFieldSpecified;
            }
            set
            {
                _importoTotaleDocumentoFieldSpecified = value;
            }
        }

        [XmlIgnore]
        public virtual decimal Arrotondamento
        {
            get
            {
                return _arrotondamentoField;
            }
            set
            {
                _arrotondamentoField = value;
                ArrotondamentoSpecified = _arrotondamentoField > (decimal) 0.0;
            }
        }

        [XmlIgnore]
        public bool ArrotondamentoSpecified
        {
            get
            {
                return _arrotondamentoFieldSpecified;
            }
            set
            {
               _arrotondamentoFieldSpecified = value;
            }
        }

        public virtual string[] Causale
        {
            get
            {
                return _causaleField;
            }
            set
            {
                _causaleField = value;
            }
        }

        public virtual Art73Type Art73
        {
            get
            {
                return _art73Field;
            }
            set
            {
                _art73Field = value;
                Art73Specified = _art73Field == Art73Type.SI;
            }
        }

        [XmlIgnore]
        public virtual bool Art73Specified
        {
            get
            {
                return _art73FieldSpecified;
            }
            set
            {
                _art73FieldSpecified = value;
            }
        }
    }
}