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

        [Description( "non soggette -OBSOLETE-" )]
        N2,

        [Description( "Operazioni non soggette ad IVA ai sensi degli artt. da 7 a 7-septies" )]
        N2_1,

        [Description( "Operazioni non soggette – altri casi" )]
        N2_2,


        [Description( "non imponibili -OBSOLETE-" )]
        [Obsolete]
        N3,

        [Description( "Operazioni non imponibili – esportazioni" )]
        N3_1,

        [Description( "Operazioni non imponibili – cessioni intracomunitarie" )]
        N3_2,

        [Description( "Operazioni non imponibili – cessioni verso San Marino" )]
        N3_3,

        [Description( "Operazioni non imponibili – operazioni assimilate alle cessioni all’esportazione" )]
        N3_4,

        [Description( "Operazioni non imponibili – a seguito di dichiarazioni d’intento" )]
        N3_5,

        [Description( "Operazioni non imponibili – altre operazioni che non concorrono alla formazione del plafond" )]
        N3_6,

        [Description( "Operazioni esenti" )]
        N_4,

        [Description( "Regime del margine/IVA non esposta in fattura" )]
        N5,

        [Description( "inversione contabile (reverse charge) -OBSOLETE-" )]
        [Obsolete]
        N6,

        [Description( "Inversione contabile – cessione di rottami e altri materiali di recupero" )]
        N6_1,

        [Description( "Inversione contabile – cessione di oro e argento puro" )]
        N6_2,

        [Description( "Inversione contabile – subappalto nel settore edile" )]
        N6_3,

        [Description( "Inversione contabile – cessione di fabbricati" )]
        N6_4,

        [Description( "Inversione contabile – cessione di telefoni cellulari" )]
        N6_5,

        [Description( "Inversione contabile – cessione di prodotti elettronici" )]
        N6_6,

        [Description( "Inversione contabile – prestazioni comparto edile e settori connessi" )]
        N6_7,

        [Description( "Inversione contabile – operazioni settore energetico" )]
        N6_8,

        [Description( "Inversione contabile – altri casi" )]
        N6_9,

        [Description( "IVA assolta in altro stato UE (vendite a distanza ex art. 40 commi 3 e 4 e art. 41 comma 1 lett. b), D.L. n. 331/93; prestazione di servizi di telecomunicazioni, tele-radiodiffusione ed elettronici ex art. 7-sexies lett. f), g), DPR n. 633/72 e art. 74-sexies, DPR n. 633/72)" )]
        N7,

    }
}