using System;
using System.Globalization;
using System.Windows.Data;

namespace ERP_system.Converters
{
    public class EmployeePropertiesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter + ": " + value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as string).Replace(parameter as string + ":", "").Replace(" ", "");
        }
    }
}
