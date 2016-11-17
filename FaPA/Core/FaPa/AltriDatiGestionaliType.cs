using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class AltriDatiGestionaliType : BaseEntityFpa
    {
        private string _tipoDatoField;
        private string _riferimentoTestoField;
        private decimal _riferimentoNumeroField;
        private bool _riferimentoNumeroFieldSpecified;
        private DateTime _riferimentoDataField;
        private bool _riferimentoDataFieldSpecified;

        public virtual string TipoDato
        {
            get
            {
                return _tipoDatoField;
            }
            set
            {
                _tipoDatoField = value;
            }
        }

        public virtual string RiferimentoTesto
        {
            get
            {
                return _riferimentoTestoField;
            }
            set
            {
                _riferimentoTestoField = value;
            }
        }

        public virtual decimal RiferimentoNumero
        {
            get
            {
                return _riferimentoNumeroField;
            }
            set
            {
                _riferimentoNumeroField = value;
            }
        }

        [XmlIgnore]
        public virtual bool RiferimentoNumeroSpecified
        {
            get
            {
                return _riferimentoNumeroFieldSpecified;
            }
            set
            {
                _riferimentoNumeroFieldSpecified = value;
            }
        }

        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "date" )]
        public virtual DateTime RiferimentoData
        {
            get
            {
                return _riferimentoDataField;
            }
            set
            {
                _riferimentoDataField = value;
            }
        }

        [XmlIgnore]
        public virtual bool RiferimentoDataSpecified
        {
            get
            {
                return _riferimentoDataFieldSpecified;
            }
            set
            {
                _riferimentoDataFieldSpecified = value;
            }
        }
    }
}