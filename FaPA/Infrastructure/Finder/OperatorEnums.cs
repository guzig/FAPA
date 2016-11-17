using System;
using System.ComponentModel;

namespace FaPA.Infrastructure.Finder
{
    [Flags]
    public enum EnumOperatorEnums
    {
        [Description("")]
        NotSelected = 1 << 0,

        [Description("Nessun valore (vuoto)")]
        Null = 1 << 1,

        [Description("Almeno un valore (non vuoto)")]
        NotNull = 1 << 2,

        [Description("Uguale a")]
        Equal = 1 << 3,

        [Description("Diverso da")]
        NotEqual = 1 << 4,

        [Description("Incluso in elenco")]
        OneOf = 1 << 5,

        [Description("Non incluso in elenco")]
        NoneOf = 1 << 6
    }
 
    [Flags]
    public enum NumOperatorEnums
    {
        [Description("")]
        NotSelected = 1 << 0,

        [Description("Nessun valore (vuoto)")]
        Null = 1 << 1,

        [Description("Almeno un valore (non vuoto)")]
        NotNull = 1 << 2,

        [Description("Uguale a")]
        Equal = 1 << 3,

        [Description("Diverso da")]
        NotEqual = 1 << 4,

        [Description("Incluso in elenco")]
        OneOf = 1 << 5,

        [Description("Non incluso in elenco")]
        NoneOf = 1 << 6,

        [Description("Minore di")]
        Less = 1 << 7,

        [Description("Minore o uguale di")]
        LessOrEqual = 1 << 8,
        
        [Description("Maggiore di")]
        Greater = 1 << 9,

        [Description("Maggiore o uguale di")]
        GreaterOrEqual = 1 << 10,

        [Description("Compreso tra min e max")]
        Between = 1 << 11,

        [Description("Non compreso tra min e max")]
        NotBetween = 1 << 12

    }; 

    [Flags]
    public enum DateTimeOperatorEnums
    {
        [Description("")]
        NotSelected = 1 << 0,

        [Description("Nessun valore (vuoto)")]
        Null = 1 << 1,

        [Description("Almeno un valore (non vuoto)")]
        NotNull = 1 << 2,

        [Description("Uguale a")]
        Equal = 1 << 3,

        [Description("Diverso da")]
        NotEqual = 1 << 4,

        [Description("Incluso in elenco")]
        OneOf = 1 << 5,

        [Description("Non incluso in elenco")]
        NoneOf = 1 << 6,

        [Description("Minore di")]
        Less = 1 << 7,

        [Description("Minore o uguale di")]
        LessOrEqual = 1 << 8,

        [Description("Non minore di")]
        NotLess = 1 << 9,

        [Description("Maggiore di")]
        Greater = 1 << 10,

        [Description("Maggiore o uguale di")]
        GreaterOrEqual = 1 << 11,

        [Description("Non maggiore di")]
        NotGreater = 1 << 12,

        [Description("Compreso tra min e max")]
        Between = 1 << 13,

        [Description("Non compreso tra min e max")]
        NotBetween = 1 << 14

    };
        
    [Flags]
    public enum StringOperatorsEnums
    {

        [Description("")]
        NotSelected = 1 << 0,

        [Description( "Nessun valore (vuoto)" )]
        Null = 1 << 1,

        [Description("Almeno un valore (non vuoto)")]
        NotNull = 1 << 2,

        [Description("Uguale a")]
        Equal = 1 << 3,

        [Description("Diverso da")]
        NotEqual = 1 << 4,

        [Description("Inizia con")]
        StartWith = 1 << 5,

        [Description("Non inizia con")]
        NotStartWith = 1 << 6,

        [Description("Finisce con")]
        EndWith = 1 << 7,

        [Description("Non finisce con")]
        NotEndWith = 1 << 8,

        [Description("Contiene")]
        Contains = 1 << 9,

        [Description("Non contiene")]
        NotContains = 1 << 10,

        [Description("Incluso in elenco")]
        OneOf = 1 << 11,

        [Description("Non incluso in elenco")]
        NoneOf = 1 << 12

    };

    [Flags]
    public enum BoolOperatorEnums
    {

        [Description("")]
        NotSelected = 1 << 0,

        [Description("Sì\\Vero")]
        Sì = 1 << 1,

        [Description("No\\Falso")]
        No = 1 << 2


    };

    [Flags]
    public enum AssociationPropertyOperatorEnums
    {
        [Description("")]
        NotSelected = 1 << 0,

        [Description("Nessun valore (vuoto)")]
        Null = 1 << 1,

        [Description("Almeno un valore (non vuoto)")]
        NotNull = 1 << 2,

        [Description("Uguale a")]
        Equal = 1 << 3,

        [Description("Diverso da")]
        NotEqual = 1 << 4,

        [Description("Incluso in elenco")]
        OneOf = 1 << 5,

        [Description("Non incluso in elenco")]
        NoneOf = 1 << 6

    };

}
