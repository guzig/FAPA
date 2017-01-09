using System;
using System.ComponentModel;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public enum TipoDocumentoType
    {
        [Description("Fattura")]
        TD01,

        [Description("Acconto/Anticipo su fattura")]
        TD02,

        [Description("Acconto/Anticipo su parcella")]
        TD03,

        [Description("Nota di Credito")]
        TD04,

        [Description("Nota di Debito")]
        TD05,

        [Description("Parcella")]
        TD06
    }
}