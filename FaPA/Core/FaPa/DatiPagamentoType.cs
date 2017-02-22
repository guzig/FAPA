using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DatiPagamentoType : BaseEntityFpa
    {
        private CondizioniPagamentoType _condizioniPagamentoField;
        private DettaglioPagamentoType[] _dettaglioPagamentoField;

        [XmlElement( Form = XmlSchemaForm.Unqualified )]
        public virtual CondizioniPagamentoType CondizioniPagamento
        {
            get
            {
                return _condizioniPagamentoField;
            }
            set
            {
                if (value == _condizioniPagamentoField) return;
                _condizioniPagamentoField = value;
            }
        }
        
        [XmlElement( "DettaglioPagamento", Form = XmlSchemaForm.Unqualified )]
        public virtual DettaglioPagamentoType[] DettaglioPagamento
        {
            get
            {
                return _dettaglioPagamentoField;
            }
            set
            {
                _dettaglioPagamentoField = value;
            }
        }
    }
}