using System;

namespace Intime.OPC.Domain.Extensions
{
    /// <summary>
    /// string 扩展方法
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// if null to empty
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string NullToEmpty(this string self)
        {
            return self ?? (NullTo(null, String.Empty));
        }

        /// <summary>
        /// if null to def
        /// </summary>
        /// <param name="self"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static string NullTo(this string self, string def)
        {
            if (self == null)
            {
                self = def;
            }

            return self;
        }
    }
}
