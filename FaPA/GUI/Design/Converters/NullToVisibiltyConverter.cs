using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FaPA.GUI.Design.Converters
{
    public class NullToVisibiltyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Visibility.Collapsed; 
            var propertyInfo = value.GetType().GetProperty("Count");
            if (propertyInfo != null)
            {
                int count = (int)propertyInfo.GetValue(value, null);
                return count > 0 ? Visibility.Visible : Visibility.Collapsed; 
            }
            if (!( value is bool ) ) return Visibility.Visible; 
            return (bool) value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}