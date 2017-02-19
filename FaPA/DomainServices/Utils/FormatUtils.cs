using System.Globalization;

namespace FaPA.DomainServices.Utils
{
    public static class FormatUtils
    {
        public static decimal ToCustomFormatDecimal(this string value )
        {
            return decimal.Parse( value.Replace( "€", "" ).Replace( "$", "" ), CultureInfo.InvariantCulture );
        }
    }
}
