using System;
using System.Globalization;
using System.Windows.Data;

namespace Redesign.ValueConverters
{
    public class IsNullOrEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((value == null) || ((String)value == ""));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("IsNullOrEmptyConverter can only be used OneWay.");
        }
    }
}
