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
        TD06,

        [Description( "Integrazione fattura reverse charge interno" )]
        TD16,

        [Description( "Integrazione/autofattura per acquisto servizi dall’estero" )]
        TD17,

        [Description( "Integrazione per acquisto di beni intracomunitari" )]
        TD18,

        [Description( "Integrazione/autofattura per acquisto di beni ex art.17 c.2 DPR n. 633/72" )]
        TD19,

        [Description( "Autofattura per regolarizzazione e integrazione delle fatture (art.6 c.8 d.lgs. 471/97 o art.46 c.5 D.L. 331/93)" )]
        TD20,

        [Description( "Autofattura per splafonamento" )]
        TD21,

        [Description( "Estrazione beni da Deposito IVA" )]
        TD22,

        [Description( "Estrazione beni da Deposito IVA con versamento dell’IVA" )]
        TD23,

        [Description( "Fattura differita di cui all’art.21, comma 4, lett. a)" )]
        TD24,

        [Description( "Fattura differita di cui all’art.21, comma 4, terzo periodo lett. b)" )]
        TD25,

        [Description( "Cessione di beni ammortizzabili e per passaggi interni (ex art.36 DPR 633/72)" )]
        TD26,

        [Description( "Fattura per autoconsumo o per cessioni gratuite senza rivalsa" )]
        TD27

    }
}