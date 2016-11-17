using System;
using System.Xml.Schema;
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

        private bool dataInizioPeriodoFieldSpecified;

        private DateTime dataFinePeriodoField;

        private bool dataFinePeriodoFieldSpecified;

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
        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "integer" )]
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

        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "normalizedString" )]
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

        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "normalizedString" )]
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

        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "date" )]
        public virtual DateTime DataInizioPeriodo
        {
            get
            {
                return dataInizioPeriodoField;
            }
            set
            {
                dataInizioPeriodoField = value;
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

        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "date" )]
        public virtual DateTime DataFinePeriodo
        {
            get
            {
                return dataFinePeriodoField;
            }
            set
            {
                dataFinePeriodoField = value;
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

        [XmlElement( "ScontoMaggiorazione", Form = XmlSchemaForm.Unqualified )]
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

        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "normalizedString" )]
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

        [XmlElement( "AltriDatiGestionali", Form = XmlSchemaForm.Unqualified )]
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