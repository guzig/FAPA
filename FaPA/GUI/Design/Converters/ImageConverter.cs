using System;
using System.Globalization;
using System.Windows.Data;

namespace FaPA.GUI.Design.Converters
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Design/Styles/Images/wizard
             
            //return "/Images/" + value.ToString() + ".png";

            var  id = (int)value;

            switch ( id )
            {
                case 1:
                    return "/Design/Styles/Images/Bar-Chart-icon.png";
                case 2:
                    return "/Design/Styles/Images/Bar-Chart-icon.png";
                case 3:
                    return "/Design/Styles/Images/Chart-column-icon.png";
                case 4:
                    return "/Design/Styles/Images/Chart-column-icon.png";
                case 5:
                    return "/Design/Styles/Images/Chart-pie-icon.png";
                case 7:
                    return "/Design/Styles/Images/currency_blue_euro.png";
                case 8:
                    return "/Design/Styles/Images/Alert-icon.png";
                case 9:
                    return "/Design/Styles/Images/Line-chart-icon.png";
                case 10:
                    return "/Design/Styles/Images/Business-graph-icon.png";

                default: 
                    return "";
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    } 
}
