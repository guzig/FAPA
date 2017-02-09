using System;
using System.ComponentModel;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public enum SoggettoEmittenteType
    {
        [Description( "cessionario/committente" )]
        CC,

        [Description( "soggetto terzo" )]
        TZ
    }
}