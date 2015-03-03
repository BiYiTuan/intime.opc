using System.ComponentModel;

namespace Intime.OPC.Domain.Enums
{
    /// <summary>
    ///     销售状态
    /// </summary>
    public enum EnumSaleStatus
    {
        /// <summary>
        ///     The sold
        /// </summary>
        [Description("已销售")] Sold = 0,

        /// <summary>
        ///     The valid
        /// </summary>
        [Description("有效的")] Valid = 5
    }
}