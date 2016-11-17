using System;
using System.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DatiDocumentiCorrelatiType : BaseEntityFpa
    {
        private string[] _riferimentoNumeroLineaField;
        private string _idDocumentoField;
        private DateTime _dataField;
        private bool _dataFieldSpecified;
        private string _numItemField;
        private string _codiceCommessaConvenzioneField;
        private string _codiceCupField;
        private string _codiceCigField;
        
        //[XmlIgnore]
        public virtual string[] RiferimentoNumeroLinea
        {
            get
            {
                return _riferimentoNumeroLineaField;
            }
            set
            {
                _riferimentoNumeroLineaField = value != null && !value.Any() ? null : value;
            }
        }

        public virtual string IdDocumento
        {
            get
            {
                return _idDocumentoField;
            }
            set
            {
                _idDocumentoField = value;
            }
        }
        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "date" )]
        public virtual DateTime Data
        {
            get
            {
                return _dataField;
            }
            set
            {
                _dataField = value;
            }
        }
        
        [XmlIgnore]
        public virtual bool DataSpecified
        {
            get
            {
                return _dataFieldSpecified;
            }
            set
            {
                _dataFieldSpecified = value;
            }
        }
       
        public virtual string NumItem
        {
            get
            {
                return _numItemField;
            }
            set
            {
                _numItemField = value;
            }
        }

        public virtual string CodiceCommessaConvenzione
        {
            get
            {
                return _codiceCommessaConvenzioneField;
            }
            set
            {
                _codiceCommessaConvenzioneField = value;
            }
        }

        public virtual string CodiceCup
        {
            get
            {
                return _codiceCupField;
            }
            set
            {
                _codiceCupField = value;
            }
        }

        public virtual string CodiceCig
        {
            get
            {
                return _codiceCigField;
            }
            set
            {
                _codiceCigField = value;
            }
        }
    }
}