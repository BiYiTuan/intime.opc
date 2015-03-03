using System;
using System.ComponentModel;
using System.Reflection;

namespace Intime.OPC.Domain.Extensions
{
    /// <summary>
    ///     Class EnumExtensions
    /// </summary>
    public static class EnumExtensions
    {
        #region Methods

        /// <summary>
        ///     获取枚举类型的描述信息
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            if (field == null)
            {
                return value.AsId().ToString();
            }
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false)
                as DescriptionAttribute[];

            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        /// <summary>
        ///     获得枚举类型的值
        /// </summary>
        /// <param name="enumeration">The enumeration.</param>
        /// <returns>System.Int32.</returns>
        public static int AsId(this Enum enumeration)
        {
            return Convert.ToInt32(enumeration);
        }

        #endregion Methods
    }
}
