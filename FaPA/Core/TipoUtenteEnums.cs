using System;
using System.ComponentModel;

namespace FaPA.Core
{
    [Flags]
    public enum TipoUtenteEnums
    {
        [Description("Operatore")]
        User = 0,

        [Description("Amministratore")]
        Administrators = 1 << 0

    }
}
