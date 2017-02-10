using System;
using System.ComponentModel;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public enum EsigibilitaIVAType
    {
        [Description("scissione dei pagamenti")]
        S,

        [Description("IVA ad esigibilità differita")]
        D,

        [Description("IVA ad esigibilità immediata")]
        I,

        [Description("")]
        N

    }
}