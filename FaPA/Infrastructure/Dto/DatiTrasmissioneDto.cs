using FaPA.Core.FaPa;

namespace FaPA.Infrastructure.Dto
{
    public class DatiTrasmissioneDto : NotDaoBaseEntityDto
    {
        private string _telefonoField;
        private string _emailField;
        private string _progressivoInvioField;
        private FormatoTrasmissioneType _formatoTrasmissioneField ;
        private string _codiceDestinatarioField;

        private string _idPaese;
        public string IdPaese
        {
            get
            {
                return _idPaese;
            }
            set
            {
                if ( value == _idPaese) return;
                _idPaese = value;
                
            }
        }

        private string _idCodice;
        public string IdCodice
        {
            get
            {
                return _idCodice;
            }
            set
            {
                if ( value == _idCodice ) return;
                _idCodice = value;
                
            }
        }

        public string ProgressivoInvio
        {
            get
            {
                return _progressivoInvioField;
            }
            set
            {
                if ( value == _progressivoInvioField ) return;
                _progressivoInvioField = value;
                
            }
        }

        public FormatoTrasmissioneType FormatoTrasmissione
        {
            get
            {
                return _formatoTrasmissioneField;
            }
            set
            {
                if ( value == _formatoTrasmissioneField ) return;
                _formatoTrasmissioneField = value;
                
            }
        }

        public string CodiceDestinatario
        {
            get
            {
                return _codiceDestinatarioField;
            }
            set
            {
                if ( value == _codiceDestinatarioField ) return;
                _codiceDestinatarioField = value;
                
            }
        }

        public string Telefono
        {
            get
            {
                return _telefonoField;
            }
            set
            {
                if ( value == _telefonoField ) return;
                _telefonoField = value;
                
            }
        }

        public string Email
        {
            get
            {
                return _emailField;
            }
            set
            {
                if ( value == _emailField ) return;
                _emailField = value;
                
            }
        }
        
        public override bool IsProxy()
        {
            return false;
        }


    }
}