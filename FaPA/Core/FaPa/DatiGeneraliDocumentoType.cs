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

        private TipoDocumentoType tipoDocumentoField;

        private string divisaField;

        private DateTime dataField;

        private string numeroField;

        private DatiRitenutaType datiRitenutaField = new DatiRitenutaType();

        private DatiBolloType datiBolloField = new DatiBolloType();

        private DatiCassaPrevidenzialeType datiCassaPrevidenzialeField = new DatiCassaPrevidenzialeType();

        private ScontoMaggiorazioneType[] scontoMaggiorazioneField;

        private decimal importoTotaleDocumentoField;

        private bool importoTotaleDocumentoFieldSpecified;

        private decimal arrotondamentoField;

        private bool arrotondamentoFieldSpecified;

        private string[] causaleField;

        private Art73Type art73Field;

        private bool art73FieldSpecified;


        #endregion

        public virtual TipoDocumentoType TipoDocumento
        {
            get
            {
                return tipoDocumentoField;
            }
            set
            {
                tipoDocumentoField = value;
            }
        }

        public virtual string Divisa
        {
            get
            {
                return divisaField;
            }
            set
            {
                divisaField = value;
            }
        }

        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "date" )]
        public virtual DateTime Data
        {
            get
            {
                return dataField;
            }
            set
            {
                dataField = value;
            }
        }

        public virtual string Numero
        {
            get
            {
                return numeroField;
            }
            set
            {
                numeroField = value;
            }
        }

        [XmlIgnore]
        public virtual bool DatiRitenutaSpecified { get; set; }  
        
        public virtual DatiRitenutaType DatiRitenuta
        {
            get
            {
                return datiRitenutaField;
            }
            set
            {
                datiRitenutaField = value;
                DatiRitenutaSpecified = DatiRitenuta.AliquotaRitenuta != 0 || DatiRitenuta.ImportoRitenuta != 0;
            }
        }

        [XmlIgnore]
        public bool DatiBolloSpecified { get; set; }

        public virtual DatiBolloType DatiBollo
        {
            get
            {
                return datiBolloField;
            }
            set
            {
                datiBolloField = value;
                DatiBolloSpecified = DatiBollo.ImportoBollo > 0;
            }
        }

        [XmlIgnore]
        public virtual bool DatiCassaPrevidenzialeSpecified { get; set; }

        public virtual DatiCassaPrevidenzialeType DatiCassaPrevidenziale
        {
            get
            {
                return datiCassaPrevidenzialeField;
            }
            set
            {
                datiCassaPrevidenzialeField = value;
                DatiCassaPrevidenzialeSpecified = datiCassaPrevidenzialeField.ImportoContributoCassa != 0;
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

        public virtual decimal ImportoTotaleDocumento
        {
            get
            {
                return importoTotaleDocumentoField;
            }
            set
            {
                importoTotaleDocumentoField = value;
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
                return arrotondamentoField;
            }
            set
            {
                arrotondamentoField = value;
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
                return causaleField;
            }
            set
            {
                causaleField = value;
            }
        }

        public virtual Art73Type Art73
        {
            get
            {
                return art73Field;
            }
            set
            {
                art73Field = value;
            }
        }

        [XmlIgnore]
        public virtual bool Art73Specified
        {
            get
            {
                return art73FieldSpecified;
            }
            set
            {
                art73FieldSpecified = value;
            }
        }
    }
}