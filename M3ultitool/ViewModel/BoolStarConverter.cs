using System;
using System.Globalization;
using System.Windows.Data;

namespace M3ultitool.ViewModel
{
    public class BoolStarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool) && (bool)value ? "*" : "";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value as string ?? "";
            return str == "*";
        }
    }
}
