using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPCAPP.Common.Extensions
{
    public static class ReflectionExtension
    {
        public static TCustomAttribute GetCustomAttribute<TCustomAttribute>(this Type type)
        {
            var attributes = type.GetCustomAttributes(typeof(TCustomAttribute), true);

            return attributes.Cast<TCustomAttribute>().FirstOrDefault();
        }
    }
}
