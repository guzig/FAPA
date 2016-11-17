using System;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DatiCassaPrevidenzialeType : BaseEntityFpa
    {
        #region fields

        private TipoCassaType tipoCassaField;

        private decimal alCassaField;

        private decimal importoContributoCassaField;

        private decimal imponibileCassaField;

        private bool imponibileCassaFieldSpecified;

        private decimal aliquotaIVAField;

        private RitenutaType ritenutaField;

        private bool ritenutaFieldSpecified;

        private NaturaType naturaField;

        private bool naturaFieldSpecified;

        private string riferimentoAmministrazioneField;

        
        #endregion

        public virtual TipoCassaType TipoCassa
        {
            get
            {
                return tipoCassaField;
            }
            set
            {
                if (value == tipoCassaField) return;
                tipoCassaField = value;
            }
        }

        public virtual decimal AlCassa
        {
            get
            {
                return alCassaField;
            }
            set
            {
                if (value == alCassaField) return;
                alCassaField = value;
            }
        }

        public virtual decimal ImportoContributoCassa
        {
            get
            {
                return importoContributoCassaField;
            }
            set
            {
                if (value == importoContributoCassaField) return;
                importoContributoCassaField = value;
            }
        }

        public virtual decimal ImponibileCassa
        {
            get
            {
                return imponibileCassaField;
            }
            set
            {
                if (value == imponibileCassaField) return;
                imponibileCassaField = value;
            }
        }

        public virtual bool ImponibileCassaSpecified
        {
            get
            {
                return imponibileCassaFieldSpecified;
            }
            set
            {
                if (value == imponibileCassaFieldSpecified) return;
                imponibileCassaFieldSpecified = value;
            }
        }

        public virtual decimal AliquotaIVA
        {
            get
            {
                return aliquotaIVAField;
            }
            set
            {
                if (value == aliquotaIVAField) return;
                aliquotaIVAField = value;
            }
        }

        public RitenutaType Ritenuta
        {
            get
            {
                return ritenutaField;
            }
            set
            {
                if (value == ritenutaField) return;
                ritenutaField = value;
            }
        }

        public virtual bool RitenutaSpecified
        {
            get
            {
                return ritenutaFieldSpecified;
            }
            set
            {
                if (value == ritenutaFieldSpecified) return;
                ritenutaFieldSpecified = value;
            }
        }

        public virtual NaturaType Natura
        {
            get
            {
                return naturaField;
            }
            set
            {
                if (value == naturaField) return;
                naturaField = value;
            }
        }

        public virtual bool NaturaSpecified
        {
            get
            {
                return naturaFieldSpecified;
            }
            set
            {
                if (value == naturaFieldSpecified) return;
                naturaFieldSpecified = value;
            }
        }

        public virtual string RiferimentoAmministrazione
        {
            get
            {
                return riferimentoAmministrazioneField;
            }
            set
            {
                if (value == riferimentoAmministrazioneField) return;
                riferimentoAmministrazioneField = value;
            }
        }
    }
}