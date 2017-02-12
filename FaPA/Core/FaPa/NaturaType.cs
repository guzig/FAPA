using System;
using System.ComponentModel;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public enum NaturaType
    {
        [Description( "" )]
        N,

        [Description( "escluse ex art.15" )]
        N1,

        [Description( "non soggette" )]
        N2,

        [Description( "non imponibili" )]
        N3,

        [Description( "esenti" )]
        N4,

        [Description( "regime del margine" )]
        N5,

        [Description( "inversione contabile (reverse charge)" )]
        N6
    }
}