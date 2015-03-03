using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Intime.OPC.Infrastructure.Mvvm
{
    public class EnumerationToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return string.Empty;

            var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Any()) return ((DescriptionAttribute)attributes[0]).Description;

            throw new InvalidOperationException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
