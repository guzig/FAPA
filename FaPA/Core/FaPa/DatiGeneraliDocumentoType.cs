using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    //[XmlType( Namespace = "http://www.fatturapa.gov.it/sdi/fatturapa/v1.1" )]
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

        [XmlIgnore]
        public virtual bool DatiRitenutaSpecified { get; set; }  
        
        public virtual DatiRitenutaType DatiRitenuta
        {
            get
            {
                return _datiRitenutaField;
            }
            set
            {
                _datiRitenutaField = value;
                //DatiRitenutaSpecified = DatiRitenuta.AliquotaRitenuta != 0 || DatiRitenuta.ImportoRitenuta != 0;
            }
        }

        [XmlIgnore]
        public bool DatiBolloSpecified { get; set; }

        public virtual DatiBolloType DatiBollo
        {
            get
            {
                return _datiBolloField;
            }
            set
            {
                _datiBolloField = value;
                DatiBolloSpecified = DatiBollo.ImportoBollo > 0;
            }
        }

        [XmlIgnore]
        public virtual bool DatiCassaPrevidenzialeSpecified { get; set; }

        public virtual DatiCassaPrevidenzialeType DatiCassaPrevidenziale
        {
            get
            {
                return _datiCassaPrevidenzialeField;
            }
            set
            {
                _datiCassaPrevidenzialeField = value;
                DatiCassaPrevidenzialeSpecified = _datiCassaPrevidenzialeField.ImportoContributoCassa != 0;
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
            }
            set
            {
                _importoTotaleDocumentoField = value;
            }
        }

        //[XmlIgnore]
        //public bool ImportoTotaleDocumentoSpecified
        //{
        //    get
        //    {
        //        return importoTotaleDocumentoFieldSpecified;
        //    }
        //    set
        //    {
        //        importoTotaleDocumentoFieldSpecified = value;
        //    }
        //}

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
            }
        }

        //[XmlIgnore]
        //public bool ArrotondamentoSpecified
        //{
        //    get
        //    {
        //        return arrotondamentoFieldSpecified;
        //    }
        //    set
        //    {
        //        arrotondamentoFieldSpecified = value;
        //    }
        //}

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