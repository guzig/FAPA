using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DatiDdtType : BaseEntityFpa
    {
        private string _numeroDdtField;
        private DateTime _dataDdtField;
        private string[] _riferimentoNumeroLineaField;
        
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
        
        public virtual string[] RiferimentoNumeroLinea
        {
            get
            {
                return _riferimentoNumeroLineaField;
            }
            set
            {
                _riferimentoNumeroLineaField = value;
            }
        }
    }
}