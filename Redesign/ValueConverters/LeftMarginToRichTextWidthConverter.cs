using System;
using System.Globalization;
using System.Windows.Data;

namespace Redesign.ValueConverters
{
    public class LeftMarginToRichTextWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value <= 1000)
            {
                return new System.Windows.Thickness(30, 0, 0, 0);
            }
            else
            {
                var x = ((int)value - 1000) / 2;
                return new System.Windows.Thickness(x, 0, 0, 0);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("LeftMarginToRichTextWidthConverter can only be used OneWay.");
        }
    }
}
