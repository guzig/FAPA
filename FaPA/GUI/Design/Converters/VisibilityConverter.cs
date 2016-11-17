using System;
using System.Windows.Data;
using System.Windows;
using System.Windows.Documents;

namespace FaPA.GUI.Design.Converters
{
    public class VisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TextRange range = (TextRange)value;

            if (range.IsEmpty)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("Not used");
        }

        #endregion
    }
}
