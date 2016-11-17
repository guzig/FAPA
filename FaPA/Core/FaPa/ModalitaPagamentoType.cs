using System;
using System.ComponentModel;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public enum ModalitaPagamentoType
    {
        [Description("contanti")]
        MP01,

        [Description("assegno")]
        MP02,

        [Description("assegno circolare")]
        MP03,

        [Description("contanti presso tesoreria")]
        MP04,

        [Description("bonifico")]
        MP05,

        [Description("Vaglia cambiario")]
        MP06,

        [Description("Bollettino cambiario")]
        MP07,

        [Description("Carta di pagamento")]
        MP08,

        [Description("RID")]
        MP09,

        [Description("RID utenze")]
        MP10,

        [Description("RID veloce")]
        MP11,

        [Description("Riba")]
        MP12,

        [Description("MAV")]
        MP13,

        [Description("quietanza erario stato")]
        MP14,

        [Description("giroconto su conti di contabilità speciale")]
        MP15,

        [Description("domiciliazione bancaria")]
        MP16,

        [Description("domiciliazione postale")]
        MP17,

        [Description("bollettino di c/c postale")]
        MP18,

        [Description("SEPA Direct Debit")]
        MP19,

        [Description("SEPA Direct Debit CORE")]
        MP20,

        [Description("SEPA Direct Debit B2B")]
        MP21
    }
}