using System;
using System.Globalization;
using System.Windows.Data;

namespace FaPA.GUI.Design.Converters
{
    [ValueConversion(typeof(int), typeof(int))]
    public class ZeroToOneBasedIndexConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = (int)value;
            if (index == -1)
                index = 1;
            else
                index += 1;
            return index;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index = (int)value;
            index -= 1;
            return index;
        }

        #endregion
    }
}
