using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Intime.OPC.CustomControls
{
    public class String2ListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var list = new List<string>();
            if (value != null)
            {
                var str = value as string;
                if (!string.IsNullOrWhiteSpace(str))
                {
                    list = str.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).ToList();
                }
            }
            return list;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}