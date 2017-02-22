using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public class DettaglioPagamentoType : BaseEntityFpa
    {
        private decimal _importoPagamento;
        
        public virtual  string Beneficiario { get; set; }

        public virtual  ModalitaPagamentoType ModalitaPagamento{ get; set; }

        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "date" )]
        public virtual  DateTime DataRiferimentoTerminiPagamento { get; set; }

        [XmlIgnore]
        public virtual  bool DataRiferimentoTerminiPagamentoSpecified { get; set; }

        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "integer" )]
        public virtual  string GiorniTerminiPagamento { get; set; }

        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "date" )] 
        public virtual  DateTime DataScadenzaPagamento { get; set; }

        [XmlIgnore]
        public virtual  bool DataScadenzaPagamentoSpecified { get; set; }

        public virtual  decimal ImportoPagamento
        {
            get { return _importoPagamento; }
            set
            {
                if ( value == 0 )
                    _importoPagamento = value;
                else

                _importoPagamento = decimal.Parse( string.Format( "{0:0.00}", value ) ); 
            }
        }

        public virtual  string CodUfficioPostale { get; set; }

        public virtual  string CognomeQuietanzante { get; set; }

        public virtual  string NomeQuietanzante { get; set; }

        public virtual  string CfQuietanzante { get; set; }

        public virtual  string TitoloQuietanzante { get; set; }

        public virtual  string IstitutoFinanziario { get; set; }

        public virtual  string Iban { get; set; }

        public virtual  string Abi { get; set; }

        public virtual  string Cab { get; set; }

        public virtual  string Bic { get; set; }

        public virtual  decimal ScontoPagamentoAnticipato { get; set; }

        [XmlIgnore]
        public virtual  bool ScontoPagamentoAnticipatoSpecified { get; set; }

        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "date" )]
        public virtual  DateTime DataLimitePagamentoAnticipato { get; set; }

        [XmlIgnore]
        public virtual  bool DataLimitePagamentoAnticipatoSpecified { get; set; }

        public virtual  decimal PenalitaPagamentiRitardati { get; set; }

        [XmlIgnore]
        public virtual  bool PenalitaPagamentiRitardatiSpecified { get; set; }

        [XmlElement( Form = XmlSchemaForm.Unqualified, DataType = "date" )]
        public virtual  DateTime DataDecorrenzaPenale { get; set; }

        [XmlIgnore]
        public virtual  bool DataDecorrenzaPenaleSpecified { get; set; }

        public virtual  string CodicePagamento { get; set; }
    }
}