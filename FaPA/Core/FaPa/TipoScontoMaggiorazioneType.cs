using System;
using System.ComponentModel;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public enum TipoScontoMaggiorazioneType
    {
        [Description( "Sconto" )]
        SC,
        [Description( "Maggiorazione" )]
        MG
    }
}