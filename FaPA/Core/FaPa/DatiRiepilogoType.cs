using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DatiRiepilogoType : BaseEntityFpa
    {

        #region 

        private decimal _aliquotaIvaField;

        private NaturaType _naturaField;

        private bool _naturaFieldSpecified;

        private decimal _speseAccessorieField;

        private bool _speseAccessorieFieldSpecified;

        private decimal _arrotondamentoField;

        private bool _arrotondamentoFieldSpecified;

        private decimal _imponibileImportoField;

        private decimal _impostaField;

        private EsigibilitaIVAType _esigibilitaIvaField;

        private bool _esigibilitaIvaFieldSpecified;

        private string _riferimentoNormativoField;
        
        #endregion

        public virtual decimal AliquotaIVA
        {
            get
            {
                return _aliquotaIvaField;
            }
            set
            {
                _aliquotaIvaField = value;
            }
        }

        [XmlElement( Form = XmlSchemaForm.Unqualified )]
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

        public virtual decimal SpeseAccessorie
        {
            get
            {
                return _speseAccessorieField;
            }
            set
            {
                _speseAccessorieField = value;
            }
        }

        [XmlIgnore]
        public virtual bool SpeseAccessorieSpecified
        {
            get
            {
                return _speseAccessorieFieldSpecified;
            }
            set
            {
                _speseAccessorieFieldSpecified = value;
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
                ArrotondamentoSpecified = _arrotondamentoField > 0 || _arrotondamentoField < 0;
            }
        }

        [XmlIgnore]
        public virtual bool ArrotondamentoSpecified
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

        public virtual decimal ImponibileImporto
        {
            get
            {
                return _imponibileImportoField;
            }
            set
            {
                _imponibileImportoField = decimal.Parse(string.Format("{0:0.00}", value));
            }
        }

        public virtual decimal Imposta
        {
            get
            {
                return _impostaField;
            }
            set
            {
                _impostaField = decimal.Parse(string.Format("{0:0.00}", value));
            }
        }

        public virtual EsigibilitaIVAType EsigibilitaIVA
        {
            get
            {
                return _esigibilitaIvaField;
            }
            set
            {
                _esigibilitaIvaField = value;
                EsigibilitaIVASpecified = _esigibilitaIvaField != EsigibilitaIVAType.N;
            }
        }

        [XmlIgnore]
        public virtual bool EsigibilitaIVASpecified
        {
            get
            {
                return _esigibilitaIvaFieldSpecified;
            }
            set
            {
                _esigibilitaIvaFieldSpecified = value;
            }
        }      

        public virtual string RiferimentoNormativo
        {
            get
            {
                return _riferimentoNormativoField;
            }
            set
            {
                _riferimentoNormativoField = value;
            }
        }
    }
}