using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Intime.OPC.CustomControls
{
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = true;
            try
            {
                result = System.Convert.ToBoolean(value);
            }
            catch
            {
            }

            Visibility v;
            if (result) v = Visibility.Visible;
            else v = Visibility.Collapsed;
            return v;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}