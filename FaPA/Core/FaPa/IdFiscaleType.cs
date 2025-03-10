using System;

namespace FaPA.Core.FaPa
{
    [Serializable]
    
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