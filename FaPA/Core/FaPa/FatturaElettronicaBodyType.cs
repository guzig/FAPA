using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class FatturaElettronicaBodyType : BaseEntityFpa
    {
        private DatiGeneraliType _datiGeneraliField;
        private DatiBeniServiziType _datiBeniServiziField;
        private DatiVeicoliType _datiVeicoliField;
        private DatiPagamentoType[] _datiPagamentoField;
        private AllegatiType[] _allegatiField;

        [XmlElement( Form = XmlSchemaForm.Unqualified )]
        public virtual  DatiGeneraliType DatiGenerali
        {
            get
            {
                return _datiGeneraliField;
            }
            set
            {
                _datiGeneraliField = value;
            }
        }

        [XmlElement( Form = XmlSchemaForm.Unqualified )]
        public virtual  DatiBeniServiziType DatiBeniServizi
        {
            get
            {
                return _datiBeniServiziField;
            }
            set
            {
                _datiBeniServiziField = value;
            }
        }

        [XmlElement( Form = XmlSchemaForm.Unqualified )]
        public virtual  DatiVeicoliType DatiVeicoli
        {
            get
            {
                return _datiVeicoliField;
            }
            set
            {
                _datiVeicoliField = value;
            }
        }

        [XmlElement( "DatiPagamento", Form = XmlSchemaForm.Unqualified )]
        public virtual  DatiPagamentoType[] DatiPagamento
        {
            get
            {
                return _datiPagamentoField;
            }
            set
            {
                _datiPagamentoField = value;
            }
        }

        [XmlElement( "Allegati", Form = XmlSchemaForm.Unqualified )]
        public virtual  AllegatiType[] Allegati
        {
            get
            {
                return _allegatiField;
            }
            set
            {
                _allegatiField = value;
            }
        }
    }
}