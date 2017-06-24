using System;
using System.Collections.Generic;
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

            return string.Join( Environment.NewLine, source );
        }

        private static char[] GetSeparator(object parameter)
        {
            var param = parameter as char[];
            var separator = param ?? new[] { '\n' };
            return separator;
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            var source = value as string;
            var separator = GetSeparator(parameter);
            var convertBack = source?.Split(separator);
            if (convertBack == null) return null;
            var result1 = new List<string>();
            foreach (var item in convertBack)
            {
                if ( !string.IsNullOrWhiteSpace( item ) )
                {
                    result1.Add(item.Replace("\r", "").Replace("\n", ""));
                }
            }
            return result1.ToArray();
        }
    }
}
