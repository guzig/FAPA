using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class FatturaPrincipaleType : BaseEntityFpa
    {

        private string _numeroFatturaPrincipaleField;
        private DateTime _dataFatturaPrincipaleField;
        private bool _dataFatturaPrincipaleSpecified;

        public virtual  string NumeroFatturaPrincipale
        {
            get
            {
                return _numeroFatturaPrincipaleField;
            }
            set
            {
                _numeroFatturaPrincipaleField = value;
            }
        }

        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "date" )]
        public virtual  DateTime DataFatturaPrincipale
        {
            get
            {
                return _dataFatturaPrincipaleField;
            }
            set
            {
                _dataFatturaPrincipaleField = value;
                DataFatturaPrincipaleSpecified = _dataFatturaPrincipaleField != DateTime.MinValue;
            }
        }

        [XmlIgnore]
        public bool DataFatturaPrincipaleSpecified
        {
            get { return _dataFatturaPrincipaleSpecified; }
            set { _dataFatturaPrincipaleSpecified = value; }
        }
    }
}