using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DatiVeicoliType : BaseEntityFpa
    {
        private DateTime dataField;
        private string totalePercorsoField;
        
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

        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "normalizedString" )]
        public virtual string TotalePercorso
        {
            get
            {
                return totalePercorsoField;
            }
            set
            {
                totalePercorsoField = value;
            }
        }
    }
}