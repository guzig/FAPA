using System;

namespace FaPA.Infrastructure.Dto
{
    public class DatiDocumentiCorrelatiDto : NotDaoBaseEntityDto
    {
        private string[] _riferimentoNumeroLineaField=null;
        
        private DateTime _dataField;
        private bool _dataFieldSpecified;
        private string _numItemField;
        private string _codiceCommessaConvenzioneField;
        private string _codiceCupField;
        private string _codiceCigField;

        public string[] RiferimentoNumeroLinea
        {
            get
            {
                return _riferimentoNumeroLineaField;
            }
            set
            {
                if ( Equals( value, _riferimentoNumeroLineaField ) ) return;
                _riferimentoNumeroLineaField = value;
                
            }
        }

        private string _idDocumentoField;
        public string IdDocumento
        {
            get
            {
                return _idDocumentoField;
            }
            set
            {
                if ( value == _idDocumentoField ) return;
                _idDocumentoField = value;
                
            }
        }

        public DateTime Data
        {
            get
            {
                return _dataField;
            }
            set
            {
                if ( value.Equals( _dataField ) ) return;
                _dataField = value;
                
            }
        }

        public bool DataSpecified
        {
            get
            {
                return _dataFieldSpecified;
            }
            set
            {
                if ( value == _dataFieldSpecified ) return;
                _dataFieldSpecified = value;
                
            }
        }

        public string NumItem
        {
            get
            {
                return _numItemField;
            }
            set
            {
                if ( value == _numItemField ) return;
                _numItemField = value;
                
            }
        }

        public string CodiceCommessaConvenzione
        {
            get
            {
                return _codiceCommessaConvenzioneField;
            }
            set
            {
                if ( value == _codiceCommessaConvenzioneField ) return;
                _codiceCommessaConvenzioneField = value;
                
            }
        }

        public string CodiceCup
        {
            get
            {
                return _codiceCupField;
            }
            set
            {
                if ( value == _codiceCupField ) return;
                _codiceCupField = value;
                
            }
        }

        public string CodiceCig
        {
            get
            {
                return _codiceCigField;
            }
            set
            {
                if ( value == _codiceCigField ) return;
                _codiceCigField = value;
                
            }
        }

        public override bool IsProxy()
        {
            return false;
        }

    }
}