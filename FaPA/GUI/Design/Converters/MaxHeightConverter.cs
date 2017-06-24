using System;
using System.Globalization;
using System.Windows.Data;

namespace FaPA.GUI.Design.Converters
{
    public class MaxHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double pctHeight = (double)parameter;

            if ((pctHeight <= 0.0) || (pctHeight > 100.0))
                throw new Exception("MaxHeightConverter expects parameter in the range (0,100]");

            return ((double)value * pctHeight);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
