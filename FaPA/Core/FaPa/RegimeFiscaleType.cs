using System;
using System.ComponentModel;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public enum RegimeFiscaleType
    {
        [Description( "Ordinario" )]
        RF01,

        [Description( "Contribuenti minimi (art. 1, c.96-117, L. 244/2007)" )]
        RF02,

        [Description( "Nuove iniziative produttive (art.13, L. 388/2000)" )]
        RF03,
        [Description( "Agricoltura e attività connesse e pesca (artt. 34 e 34-bis, D.P.R. 633/" )]
        RF04,
        [Description( "Vendita sali e tabacchi (art. 74, c.1, D.P.R. 633/1972)" )]
        RF05,
        [Description( "Commercio dei fiammiferi (art. 74, c.1, D.P.R. 633/1972)" )]
        RF06,
        [Description( "Editoria (art. 74, c.1, D.P.R. 633/1972)" )]
        RF07,
        [Description( "Gestione di servizi di telefonia pubblica (art. 74, c.1, D.P.R. 633/1972)" )]
        RF08,
        [Description( "Rivendita di documenti di trasporto pubblico e di sosta (art. 74, c.1, D.P.R. 633/1972)" )]
        RF09,
        [Description( "Intrattenimenti, giochi e altre attività di cui alla tariffa allegata al D.P.R. n. 640/72 (art. 74, c.6, D.P.R. 633/1972)" )]
        RF10,
        [Description( "Agenzie di viaggi e turismo (art. 74-ter, D.P.R. 633/1972)" )]
        RF11,
        [Description( "Agriturismo (art. 5, c.2, L. 413/1991)" )]
        RF12,
        [Description( "Vendite a domicilio (art. 25-bis, c.6, D.P.R. 600/1973)" )]
        RF13,
        [Description( "Rivendita di beni usati, di oggetti d’arte, d’antiquariato o da collezione (art. 36, D.L. 41/1995)" )]
        RF14,
        [Description( "Agenzie di vendite all’asta di oggetti d’arte, antiquariato o da collezione (art. 40-bis, D.L. 41/1995)" )]
        RF15,
        [Description( "IVA per cassa P.A. (art. 6, c.5, D.P.R. 633/1972)" )]
        RF16,
        [Description( "IVA per cassa (art. 32-bis, D.L. 83/2012)" )]
        RF17,
        [Description( "Altro" )]
        RF18,
        [Description( "Forfettario (art.1, c. 54-89, L. 190/2014)" )]
        RF19
    }
}