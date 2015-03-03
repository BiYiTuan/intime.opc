using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace OPCApp.Common
{
    public class String2ListConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            List<string> list = new List<string>();
            if (value != null ) {
                string str = value as string;
                if (!string.IsNullOrWhiteSpace(str)) {
                    list = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    
                }
            }
            return list;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
