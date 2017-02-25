using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace FaPA.GUI.Design.Converters
{
    [ValueConversion( typeof( string[] ), typeof( string ) )]
    public class StringArrayConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            var source = value as string[];
            if ( source == null )
                return null;

            return string.Join( ",", source );
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            var source = value as string;
            if ( source == null )
                return null;

            return source.Split( ',' ).ToArray();

        }
    }
}
