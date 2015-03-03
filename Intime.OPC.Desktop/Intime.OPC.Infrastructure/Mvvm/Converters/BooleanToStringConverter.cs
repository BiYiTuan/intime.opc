using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Intime.OPC.Infrastructure.Mvvm
{
    public class BooleanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
            {
                return (bool)value ? "是" : "否";
            }
            string[] words = parameter.ToString().Split('^');
            if (words.Length != 2) throw new ArgumentException("Invalid converter parameter.");

            return (bool)value ? words[0] : words[1];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
