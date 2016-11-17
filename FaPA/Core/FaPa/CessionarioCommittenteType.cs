using System;

namespace FaPA.Core.FaPa
{
    [Serializable]
    //[XmlType( Namespace = "http://www.fatturapa.gov.it/sdi/fatturapa/v1.1" )]
    public class CessionarioCommittenteType: BaseEntityFpa 
    {
        private DatiAnagraficiCessionarioType datiAnagraficiField;
        private IndirizzoType sedeField;

        public virtual DatiAnagraficiCessionarioType DatiAnagrafici
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

        public virtual IndirizzoType Sede
        {
            get
            {
                return sedeField;
            }
            set
            {
                sedeField = value;
            }
        }
    }
}