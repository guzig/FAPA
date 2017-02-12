using System;
using System.ComponentModel;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public enum TipoCessionePrestazioneType
    {
        [Description( "" )]
        N,

        [Description("Sconto")]
        SC,

        [Description("Premio")]
        PR,

        [Description("Abbuono")]
        AB,

        [Description("Spesa accessoria")]
        AC
    }
}