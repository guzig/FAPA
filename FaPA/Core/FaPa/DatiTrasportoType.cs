using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DatiTrasportoType : BaseEntityFpa
    {
        private DatiAnagraficiVettoreType _datiAnagraficiVettoreField;
        private IndirizzoType _indirizzoResaField;

        private string _mezzoTrasportoField;
        private string _causaleTrasportoField;
        private string _numeroColliField;
        private string _descrizioneField;
        private string _unitaMisuraPesoField;
        private decimal _pesoLordoField;
        private bool _pesoLordoFieldSpecified;
        private decimal _pesoNettoField;
        private bool _pesoNettoFieldSpecified;
        private DateTime _dataOraRitiroField;
        private bool _dataOraRitiroFieldSpecified;
        private DateTime _dataInizioTrasportoField;
        private bool _dataInizioTrasportoFieldSpecified;
        private string _tipoResaField;
        private DateTime _dataOraConsegnaField;
        private bool _dataOraConsegnaFieldSpecified;

        //[XmlIgnore]
        //public bool DatiAnagraficiVettoreSpecified { get; set; }
        
        public virtual DatiAnagraficiVettoreType DatiAnagraficiVettore
        {
            get
            {
                return _datiAnagraficiVettoreField;
            }
            set
            {
                _datiAnagraficiVettoreField = value;
            }
        }
        
        public virtual string MezzoTrasporto
        {
            get
            {
                return _mezzoTrasportoField;
            }
            set
            {
                _mezzoTrasportoField = value;
            }
        }
        
        public virtual string CausaleTrasporto
        {
            get
            {
                return _causaleTrasportoField;
            }
            set
            {
                _causaleTrasportoField = value;
            }
        }

        public virtual string NumeroColli
        {
            get
            {
                return _numeroColliField;
            }
            set
            {
                _numeroColliField = value;
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

        public virtual string UnitaMisuraPeso
        {
            get
            {
                return _unitaMisuraPesoField;
            }
            set
            {
                _unitaMisuraPesoField = value;
            }
        }

        public virtual decimal PesoLordo
        {
            get
            {
                return _pesoLordoField;
            }
            set
            {
                _pesoLordoField = value;
                PesoLordoSpecified = _pesoLordoField != 0;
            }
        }

        public virtual bool PesoLordoSpecified
        {
            get
            {
                return _pesoLordoFieldSpecified;
            }
            set
            {
                _pesoLordoFieldSpecified = value;
            }
        }

        public virtual decimal PesoNetto
        {
            get
            {
                return _pesoNettoField;
            }
            set
            {
                _pesoNettoField = value;
                PesoNettoSpecified = _pesoNettoField != 0;
            }
        }
        
        [XmlIgnore]
        public virtual bool PesoNettoSpecified
        {
            get
            {
                return _pesoNettoFieldSpecified;
            }
            set
            {
                _pesoNettoFieldSpecified = value;
            }
        }
        
        public virtual DateTime DataOraRitiro
        {
            get
            {
                return _dataOraRitiroField;
            }
            set
            {
                _dataOraRitiroField = value;
                DataOraRitiroSpecified = _dataOraRitiroField != DateTime.MinValue;
            }
        }
       
        [XmlIgnore]
        public virtual bool DataOraRitiroSpecified
        {
            get
            {
                return _dataOraRitiroFieldSpecified;
            }
            set
            {
                _dataOraRitiroFieldSpecified = value;
            }
        }
        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "date" )]
        public virtual DateTime DataInizioTrasporto
        {
            get
            {
                return _dataInizioTrasportoField;
            }
            set
            {
                _dataInizioTrasportoField = value;
                DataInizioTrasportoSpecified = _dataInizioTrasportoField != DateTime.MinValue;
            }
        }
        
        [XmlIgnore]
        public virtual bool DataInizioTrasportoSpecified
        {
            get
            {
                return _dataInizioTrasportoFieldSpecified;
            }
            set
            {
                _dataInizioTrasportoFieldSpecified = value;
            }
        }

        public virtual string TipoResa
        {
            get
            {
                return _tipoResaField;
            }
            set
            {
                _tipoResaField = value;
            }
        }

        public virtual IndirizzoType IndirizzoResa
        {
            get
            {
                return _indirizzoResaField;
            }
            set
            {
                _indirizzoResaField = value;
            }
        }

        public virtual DateTime DataOraConsegna
        {
            get
            {
                return _dataOraConsegnaField;
            }
            set
            {
                _dataOraConsegnaField = value;
                DataOraConsegnaSpecified = _dataOraConsegnaField != DateTime.MinValue;
            }
        }
        
        [XmlIgnore]
        public virtual bool DataOraConsegnaSpecified
        {
            get
            {
                return _dataOraConsegnaFieldSpecified;
            }
            set
            {
                _dataOraConsegnaFieldSpecified = value;
            }
        }
    }
}