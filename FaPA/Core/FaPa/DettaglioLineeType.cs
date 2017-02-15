using System;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DettaglioLineeType : BaseEntityFpa
    {

        #region fields

        private string numeroLineaField;

        private TipoCessionePrestazioneType tipoCessionePrestazioneField;

        private bool tipoCessionePrestazioneFieldSpecified;

        private CodiceArticoloType[] codiceArticoloField;

        private string descrizioneField;

        private decimal quantitaField;

        private bool quantitaFieldSpecified;

        private string unitaMisuraField;

        private DateTime dataInizioPeriodoField;

        private bool dataInizioPeriodoFieldSpecified=true;

        private DateTime dataFinePeriodoField;

        private bool dataFinePeriodoFieldSpecified=true;

        private decimal prezzoUnitarioField;

        private ScontoMaggiorazioneType[] scontoMaggiorazioneField;

        private decimal prezzoTotaleField;

        private decimal aliquotaIVAField;

        private RitenutaType ritenutaField;

        private bool ritenutaFieldSpecified;

        private NaturaType naturaField;

        private bool naturaFieldSpecified=true;

        private string riferimentoAmministrazioneField;

        private AltriDatiGestionaliType[] altriDatiGestionaliField;

        #endregion
        
        public virtual  string NumeroLinea
        {
            get
            {
                return numeroLineaField;
            }
            set
            {
                numeroLineaField = value;
            }
        }

        [XmlIgnore]
        public virtual TipoCessionePrestazioneType TipoCessionePrestazione
        {
            get
            {
                return tipoCessionePrestazioneField;
            }
            set
            {
                tipoCessionePrestazioneField = value;
                TipoCessionePrestazioneSpecified = tipoCessionePrestazioneField != TipoCessionePrestazioneType.N;
            }
        }

        [XmlIgnore]
        public virtual bool TipoCessionePrestazioneSpecified
        {
            get
            {
                return tipoCessionePrestazioneFieldSpecified;
            }
            set
            {
                tipoCessionePrestazioneFieldSpecified = value;
            }
        }

        [XmlIgnore]
        public virtual CodiceArticoloType[] CodiceArticolo
        {
            get
            {
                return codiceArticoloField;
            }
            set
            {
                codiceArticoloField = value;
            }
        }

        public virtual string Descrizione
        {
            get
            {
                return descrizioneField;
            }
            set
            {
                descrizioneField = value;
            }
        }

        public virtual decimal Quantita
        {
            get
            {
                return quantitaField;
            }
            set
            {
                quantitaField = decimal.Parse(string.Format("{0:###0.00}",value) );
            }
        }

        //[XmlIgnore]
        //public bool QuantitaSpecified
        //{
        //    get
        //    {
        //        return quantitaFieldSpecified;
        //    }
        //    set
        //    {
        //        quantitaFieldSpecified = value;
        //    }
        //}

        public virtual string UnitaMisura
        {
            get
            {
                return unitaMisuraField;
            }
            set
            {
                unitaMisuraField = value;
            }
        }

        public virtual DateTime DataInizioPeriodo
        {
            get
            {
                return dataInizioPeriodoField;
            }
            set
            {
                dataInizioPeriodoField = value;
                DataInizioPeriodoSpecified = dataInizioPeriodoField.Year > 1;
            }
        }

        [XmlIgnore]
        public virtual bool DataInizioPeriodoSpecified
        {
            get
            {
                return dataInizioPeriodoFieldSpecified;
            }
            set
            {
                dataInizioPeriodoFieldSpecified = value;
            }
        }

        public virtual DateTime DataFinePeriodo
        {
            get
            {
                return dataFinePeriodoField;
            }
            set
            {
                dataFinePeriodoField = value;
                DataFinePeriodoSpecified = dataFinePeriodoField != DateTime.MinValue && dataFinePeriodoField != DateTime.MaxValue;
            }
        }

        [XmlIgnore]
        public virtual bool DataFinePeriodoSpecified
        {
            get
            {
                return dataFinePeriodoFieldSpecified;
            }
            set
            {
                dataFinePeriodoFieldSpecified = value;
            }
        }

        public virtual decimal PrezzoUnitario
        {
            get
            {
                return prezzoUnitarioField;
            }
            set
            {
                prezzoUnitarioField = decimal.Parse(string.Format("{0:###0.00}", value));
            }
        }

        public virtual ScontoMaggiorazioneType[] ScontoMaggiorazione
        {
            get
            {
                return scontoMaggiorazioneField;
            }
            set
            {
                scontoMaggiorazioneField = value;
            }
        }

        public virtual decimal PrezzoTotale
        {
            get
            {
                return prezzoTotaleField;
            }
            set
            {
                prezzoTotaleField = decimal.Parse(string.Format("{0:###0.00}", value));
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
                aliquotaIVAField = decimal.Parse(string.Format("{0:###0.00}", value));
            }
        }

        public virtual RitenutaType Ritenuta
        {
            get
            {
                return ritenutaField;
            }
            set
            {
                ritenutaField = value;
                RitenutaSpecified = ritenutaField == RitenutaType.SI;
            }
        }

        [XmlIgnore]
        public virtual bool RitenutaSpecified
        {
            get
            {
                return ritenutaFieldSpecified;
            }
            set
            {
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
                naturaField = value;
                NaturaSpecified = naturaField != NaturaType.N;
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

        public virtual string RiferimentoAmministrazione
        {
            get
            {
                return riferimentoAmministrazioneField;
            }
            set
            {
                riferimentoAmministrazioneField = value;
            }
        }

        public virtual AltriDatiGestionaliType[] AltriDatiGestionali
        {
            get
            {
                return altriDatiGestionaliField;
            }
            set
            {
                altriDatiGestionaliField = value;
            }
        }
    }
}