namespace FaPA.Infrastructure.Finder
{
    public sealed class Int64SearchProperty : NumericSearchProperty<long?>
    {
        public Int64SearchProperty( string propName, ObjectFinder rootFinder ): base(rootFinder, propName)
        {
            OperatorMinValue = 0L;
            OperatorMaxValue = 0L;
        }

        protected override string ValidateRange()
        {
            return  OperatorMaxValue <= OperatorMinValue ? "Digitare un intervallo valido (massimo > minimo)" : null;
        }

    }
}