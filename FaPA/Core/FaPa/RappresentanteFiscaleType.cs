using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class RappresentanteFiscaleType : BaseEntityFpa
    {

        private DatiAnagraficiRappresentanteType datiAnagraficiField;

        
        [XmlElement( Form = XmlSchemaForm.Unqualified )]
        public virtual  DatiAnagraficiRappresentanteType DatiAnagrafici
        {
            get
            {
                return datiAnagraficiField;
            }
            set
            {
                datiAnagraficiField = value;
            }
        }
    }
}