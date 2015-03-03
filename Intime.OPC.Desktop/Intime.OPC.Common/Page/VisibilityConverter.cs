using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace OPCApp.Common
{
    public class VisibilityConverter :IValueConverter{
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            bool result = true;
            try {
                result = System.Convert.ToBoolean(value);
            } catch {}

            Visibility v;
            if (result) v = Visibility.Visible;
            else v = Visibility.Collapsed;
            return v;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
