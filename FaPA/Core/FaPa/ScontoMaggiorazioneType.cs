using System;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class ScontoMaggiorazioneType : BaseEntityFpa
    {
        private TipoScontoMaggiorazioneType _tipoField;
        private decimal _percentualeField;
        private bool _percentualeFieldSpecified;
        private decimal _importoField;
        private bool _importoFieldSpecified;
        public virtual  TipoScontoMaggiorazioneType Tipo
        {
            get
            {
                return _tipoField;
            }
            set
            {
                _tipoField = value;
            }
        }

        public virtual  decimal Percentuale
        {
            get
            {
                return _percentualeField;
            }
            set
            {
                _percentualeField = decimal.Parse( string.Format( "{0:###0.00#}", value ) );
                PercentualeSpecified = _percentualeField != 0;
            }
        }
        
        [XmlIgnore]
        public virtual  bool PercentualeSpecified
        {
            get
            {
                return _percentualeFieldSpecified;
            }
            set
            {
                _percentualeFieldSpecified = value;
            }
        }

        public virtual decimal Importo
        {
            get
            {
                return _importoField;
            }
            set
            {
                _importoField = decimal.Parse( string.Format( "{0:###0.00}", value ) ); 
                ImportoSpecified = _importoField != 0;
            }
        }
        
        [XmlIgnore]
        public virtual  bool ImportoSpecified
        {
            get
            {
                return _importoFieldSpecified;
            }
            set
            {
                _importoFieldSpecified = value;
            }
        }
    }
}