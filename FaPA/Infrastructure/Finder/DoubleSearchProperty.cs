namespace FaPA.Infrastructure.Finder
{
    public sealed class DoubleSearchProperty : NumericSearchProperty<double?>
    {
        public DoubleSearchProperty( string propName, ObjectFinder rootFinder )
            : base( rootFinder, propName )
        {
            OperatorMinValue = 0.0;
            OperatorMaxValue = 0.0;
        }

        protected override string ValidateRange()
        {
            return !OperatorMaxValue.HasValue || OperatorMaxValue <= OperatorMinValue
                       ? "Digitare un intervallo valido (massimo > minimo)"
                       : null;
        }

    }
}