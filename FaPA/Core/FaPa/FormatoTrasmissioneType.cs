using System;
using System.ComponentModel;

namespace FaPA.Core.FaPa
{
    [Serializable]
    
    public enum FormatoTrasmissioneType
    {
        [Description( "PA" )]
        FPA12,

        [Description( "Privato" )]
        FPR12
    }
}