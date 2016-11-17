using System;
using System.ComponentModel;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public enum CondizioniPagamentoType
    {
        [Description("pagamento a rate")]
        TP01,
        [Description("pagamento completo")]
        TP02,
        [Description("anticipo")]
        TP03
    }
}