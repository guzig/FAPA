using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FaPA.GUI.Design.Converters
{
    [ValueConversion(typeof(string), typeof(Visibility))]
    class StringMemberNameToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var targetProperty = parameter as string;
            var allowedProps = value as string;

            if (string.IsNullOrWhiteSpace(targetProperty) || string.IsNullOrWhiteSpace(allowedProps))
                return Visibility.Collapsed;

            if (allowedProps.Contains(targetProperty))
                return Visibility.Visible;
            
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();

        }
    }
}