using System;

namespace FaPA.Infrastructure.Dto
{
    public class AltriDatiDto : NotDaoBaseEntityDto
    {
        private string _tipoDatoField;
        private string _riferimentoTestoField;
        private DateTime? _riferimentoDataField;

        public string TipoDato
        {
            get
            {
                return _tipoDatoField;
            }
            set
            {
                if ( value == _tipoDatoField ) return;
                _tipoDatoField = value;
                
            }
        }

        public string RiferimentoTesto
        {
            get
            {
                return _riferimentoTestoField;
            }
            set
            {
                if ( value == _riferimentoTestoField ) return;
                _riferimentoTestoField = value;
                
            }
        }

        public DateTime? RiferimentoData
        {
            get
            {
                return _riferimentoDataField;
            }
            set
            {
                if ( value.Equals( _riferimentoDataField ) ) return;
                _riferimentoDataField = value;
                
            }
        }

        public bool RiferimentoDataSpecified => RiferimentoData.HasValue;

        public override bool IsProxy()
        {
            return false;
        }

        //public override string Error
        //{
        //    get
        //    {
        //        if (string.IsNullOrWhiteSpace(TipoDato))
        //            return "Tipo Dato non valorizzato";
        //        return null;

        //    }

        //}
    }
}