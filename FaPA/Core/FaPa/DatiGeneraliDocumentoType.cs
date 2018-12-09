using System;
using System.Globalization;
using System.Linq;
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

        private string[] _causaleField = new string[1];

        private Art73Type _art73Field;

        private bool _art73FieldSpecified;
        private bool _causaleSpecified;

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
                DatiBolloSpecified = DatiBollo.BolloVirtuale == BolloVirtualeType.SI;
            }
        }

        [XmlIgnore]
        public bool DatiBolloSpecified { get; set; }

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

        //[XmlIgnore]
        //public bool DatiCassaPrevidenzialeSpecified { get; set; }

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

        public virtual decimal Arrotondamento
        {
            get
            {
                return _arrotondamentoField;
            }
            set
            {
                _arrotondamentoField = value;
                ArrotondamentoSpecified = _arrotondamentoField > (decimal) 0.0 || _arrotondamentoField < ( decimal ) 0.0;
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "string")]
        public virtual string[] Causale
        {
            get
            {
                return _causaleField;
            }
            set
            {
                _causaleField = value;
                CausaleSpecified = _causaleField != null && _causaleField.Any();
            }
        }

        [XmlIgnore]
        public bool CausaleSpecified
        {
            get { return _causaleSpecified; }
            set { _causaleSpecified = value; }
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