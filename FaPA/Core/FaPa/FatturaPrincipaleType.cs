using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class FatturaPrincipaleType : BaseEntityFpa
    {

        private string numeroFatturaPrincipaleField;

        private DateTime dataFatturaPrincipaleField;

        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "normalizedString" )]
        public virtual  string NumeroFatturaPrincipale
        {
            get
            {
                return numeroFatturaPrincipaleField;
            }
            set
            {
                numeroFatturaPrincipaleField = value;
            }
        }

        
        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "date" )]
        public virtual  DateTime DataFatturaPrincipale
        {
            get
            {
                return dataFatturaPrincipaleField;
            }
            set
            {
                dataFatturaPrincipaleField = value;
            }
        }
    }
}