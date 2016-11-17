using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace FaPA.GUI.Design.Converters
{
    public class StringToDataTemplateConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return InternalConvert(value, targetType, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static object InternalConvert(object value, Type targetType, object parameter)
        {
            var resourseKey = value as string;

            if ( resourseKey == null )
            {
                return null;
            }
            
            var resources = Application.Current.Resources.MergedDictionaries.ToList();

            foreach (var dict in resources)
            {
                foreach (var objkey in dict.Keys)
                {
                    if ( objkey.ToString() == resourseKey )
                    {
                        return dict[objkey] as DataTemplate;
                    }
                }
            }

            return null;
        }
    }
}

