namespace FaPA.Infrastructure.Finder
{
    public sealed class FloatSearchProperty : NumericSearchProperty<float?>
    {
        public FloatSearchProperty( string propName, ObjectFinder rootFinder )
            : base( rootFinder, propName )
        {
            OperatorMinValue = 0.0F;
            OperatorMaxValue = 0.0F;
        }

        protected override string ValidateRange()
        {
            const string msg = "Digitare un intervallo valido (massimo > minimo)";
            return !OperatorMaxValue.HasValue || OperatorMaxValue <= OperatorMinValue ? msg : null;
        }

    }
}