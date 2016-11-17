using System;

namespace FaPA.Core.FaPa
{
    [Serializable]
    //[XmlType( Namespace = "http://www.fatturapa.gov.it/sdi/fatturapa/v1.1" )]
    public class AnagraficaType : BaseEntityFpa
    {
        private string titoloField;
        private string codEORIField;

        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string Denominazione { get; set; }

        public virtual string Titolo
        {
            get
            {
                return titoloField;
            }
            set
            {
                titoloField = value;
            }
        }

        public virtual string CodEORI
        {
            get
            {
                return codEORIField;
            }
            set
            {
                codEORIField = value;
            }
        }
    }
}