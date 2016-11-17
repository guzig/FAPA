using System;

namespace FaPA.Core.FaPa
{
    [Serializable]
    //[XmlType( Namespace = "http://www.fatturapa.gov.it/sdi/fatturapa/v1.1" )]
    public class IdFiscaleType : BaseEntityFpa
    {
        private string _idPaeseField;

        private string _idCodiceField;

        public virtual string IdPaese
        {
            get
            {
                return _idPaeseField;
            }
            set
            {
                _idPaeseField = value;
            }
        }

        public virtual string IdCodice
        {
            get
            {
                return _idCodiceField;
            }
            set
            {
                _idCodiceField = value;
            }
        }
    }
}