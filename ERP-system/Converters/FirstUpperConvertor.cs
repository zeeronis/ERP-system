using System;
using System.Globalization;
using System.Windows.Data;

namespace ERP_system.Converters
{
    public class FirstUpperConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value as string == null) return value;
            return char.ToUpper((value as string)[0]) + (value as string).Substring(1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value as string == null) return value;
            return char.ToLower((value as string)[0]) + (value as string).Substring(1); ;
        }
    }
}
