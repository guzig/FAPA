using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FaPA.GUI.Design.Converters
{
    public class ProgBarVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Math.Abs((double)value - 100) < 0.001 ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
