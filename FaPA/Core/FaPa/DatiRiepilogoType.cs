using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DatiRiepilogoType : BaseEntityFpa
    {

        #region 

        private decimal aliquotaIVAField;

        private NaturaType naturaField;

        private bool naturaFieldSpecified;

        private decimal speseAccessorieField;

        private bool speseAccessorieFieldSpecified;

        private decimal arrotondamentoField;

        private bool arrotondamentoFieldSpecified;

        private decimal imponibileImportoField;

        private decimal impostaField;

        private EsigibilitaIVAType esigibilitaIVAField;

        private bool esigibilitaIVAFieldSpecified;

        private string riferimentoNormativoField;
        
        #endregion

        public virtual decimal AliquotaIVA
        {
            get
            {
                return aliquotaIVAField;
            }
            set
            {
                aliquotaIVAField = value;
            }
        }

        [XmlElement( Form = XmlSchemaForm.Unqualified )]
        public virtual NaturaType Natura
        {
            get
            {
                return naturaField;
            }
            set
            {
                naturaField = value;
            }
        }

        [XmlIgnore]
        public virtual bool NaturaSpecified
        {
            get
            {
                return naturaFieldSpecified;
            }
            set
            {
                naturaFieldSpecified = value;
            }
        }

        public virtual decimal SpeseAccessorie
        {
            get
            {
                return speseAccessorieField;
            }
            set
            {
                speseAccessorieField = value;
            }
        }

        [XmlIgnore]
        public virtual bool SpeseAccessorieSpecified
        {
            get
            {
                return speseAccessorieFieldSpecified;
            }
            set
            {
                speseAccessorieFieldSpecified = value;
            }
        }

        [XmlElement( Form = XmlSchemaForm.Unqualified )]
        public virtual decimal Arrotondamento
        {
            get
            {
                return arrotondamentoField;
            }
            set
            {
                arrotondamentoField = value;
            }
        }

        [XmlIgnore]
        public virtual bool ArrotondamentoSpecified
        {
            get
            {
                return arrotondamentoFieldSpecified;
            }
            set
            {
                arrotondamentoFieldSpecified = value;
            }
        }

        public virtual decimal ImponibileImporto
        {
            get
            {
                return imponibileImportoField;
            }
            set
            {
                imponibileImportoField = decimal.Parse(string.Format("{0:###0.00}", value));
            }
        }

        public virtual decimal Imposta
        {
            get
            {
                return impostaField;
            }
            set
            {
                impostaField = decimal.Parse(string.Format("{0:###0.00}", value));
            }
        }

        public virtual EsigibilitaIVAType EsigibilitaIVA
        {
            get
            {
                return esigibilitaIVAField;
            }
            set
            {
                esigibilitaIVAField = value;
            }
        }

        [XmlIgnore]
        public virtual bool EsigibilitaIVASpecified
        {
            get
            {
                return esigibilitaIVAFieldSpecified;
            }
            set
            {
                esigibilitaIVAFieldSpecified = value;
            }
        }
        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "normalizedString" )]
        public virtual string RiferimentoNormativo
        {
            get
            {
                return riferimentoNormativoField;
            }
            set
            {
                riferimentoNormativoField = value;
            }
        }
    }
}