using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FaPA.GUI.Design.Converters
{
    public class NullToInverseVisibiltyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Visibility.Visible;
            var propertyInfo = value.GetType().GetProperty("Count");
            if (propertyInfo != null)
            {
                int count = (int)propertyInfo.GetValue(value, null);
                return count > 0 ? Visibility.Collapsed : Visibility.Visible;
            }
            if ( !( value is bool ) ) return Visibility.Collapsed;
            return (bool)value ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}