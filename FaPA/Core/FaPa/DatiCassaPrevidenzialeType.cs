using System;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DatiCassaPrevidenzialeType : BaseEntityFpa
    {
        #region fields

        private TipoCassaType _tipoCassaField;

        private decimal _alCassaField;

        private decimal _importoContributoCassaField;

        private decimal _imponibileCassaField;

        private bool _imponibileCassaFieldSpecified;

        private decimal _aliquotaIvaField;

        private RitenutaType _ritenutaField;

        private bool _ritenutaFieldSpecified;

        private NaturaType _naturaField;

        private bool _naturaFieldSpecified=true;

        private string _riferimentoAmministrazioneField;

        
        #endregion

        public virtual TipoCassaType TipoCassa
        {
            get
            {
                return _tipoCassaField;
            }
            set
            {
                if (value == _tipoCassaField) return;
                _tipoCassaField = value;
            }
        }

        public virtual decimal AlCassa
        {
            get
            {
                return _alCassaField;
            }
            set
            {
                _alCassaField = decimal.Parse( string.Format( "{0:0.00}", value ) );
            }
        }

        public virtual decimal ImportoContributoCassa
        {
            get
            {
                return _importoContributoCassaField;
            }
            set
            {
                _importoContributoCassaField = decimal.Parse( string.Format( "{0:0.00}", value ) );
            }
        }

        public virtual decimal ImponibileCassa
        {
            get
            {
                return _imponibileCassaField;
            }
            set
            {
                _imponibileCassaField = decimal.Parse( string.Format( "{0:0.00}", value ) );
                if ( _imponibileCassaField > 0 )
                    ImponibileCassaSpecified = true;
            }
        }

        [XmlIgnore]
        public virtual bool ImponibileCassaSpecified
        {
            get
            {
                return _imponibileCassaFieldSpecified;
            }
            set
            {
                if (value == _imponibileCassaFieldSpecified) return;
                _imponibileCassaFieldSpecified = value;
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
                _aliquotaIvaField = decimal.Parse( string.Format( "{0:0.00}", value ) ); 
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
                if (value == _ritenutaField) return;
                _ritenutaField = value;
                if ( Ritenuta == RitenutaType.SI )
                    NaturaSpecified = true;

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
                if (value == _ritenutaFieldSpecified) return;
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
                if (value == _naturaField) return;
                _naturaField = value;
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
                if (value == _naturaFieldSpecified) return;
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
                if (value == _riferimentoAmministrazioneField) return;
                _riferimentoAmministrazioneField = value;
            }
        }
    }
}