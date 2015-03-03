using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Infrastructure.Utilities
{
    public static class EnumerationUtility
    {
        public static IList<KeyValuePair<int, string>> ToList<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumeration type");
            }

            var list = new List<KeyValuePair<int, string>>();
            var values = Enum.GetValues(typeof(T));
            foreach (var value in values)
            {
                var fieldInfo = value.GetType().GetField(value.ToString());

                var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes != null && attributes.Any())
                {
                    var description = ((DescriptionAttribute)attributes[0]).Description;
                    list.Add(new KeyValuePair<int,string>((int)value, description));
                }
                else
                {
                    list.Add(new KeyValuePair<int, string>((int)value, fieldInfo.Name));
                }
            }

            return list;
        }
    }
}
