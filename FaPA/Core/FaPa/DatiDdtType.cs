using System;
using System.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DatiDDTType : BaseEntityFpa
    {
        private string _numeroDdtField;
        private DateTime _dataDdtField;
        private string[] _riferimentoNumeroLineaField;
        private bool _riferimentoNumeroLineaSpecified;

        public virtual string NumeroDDT
        {
            get
            {
                return _numeroDdtField;
            }
            set
            {
                _numeroDdtField = value;
            }
        }
        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "date" )]
        public virtual DateTime DataDDT
        {
            get
            {
                return _dataDdtField;
            }
            set
            {
                _dataDdtField = value;
            }
        }

        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "string" )]
        public virtual string[] RiferimentoNumeroLinea
        {
            get
            {
                return _riferimentoNumeroLineaField;
            }
            set
            {
                _riferimentoNumeroLineaField = value;
                RiferimentoNumeroLineaSpecified = _riferimentoNumeroLineaField != null &&
                                                  _riferimentoNumeroLineaField.Any( s => !string.IsNullOrWhiteSpace( s ) );
            }
        }

        [XmlIgnore]
        public bool RiferimentoNumeroLineaSpecified
        {
            get { return _riferimentoNumeroLineaSpecified; }
            set { _riferimentoNumeroLineaSpecified = value; }
        }
    }
}