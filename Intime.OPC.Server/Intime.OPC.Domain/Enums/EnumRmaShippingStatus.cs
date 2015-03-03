using System.ComponentModel;

namespace Intime.OPC.Domain.Enums
{
    /// <summary>
    ///     退货快递单状态
    /// </summary>
    public enum EnumRmaShippingStatus
    {
        [Description("未发货")] NoPrint = 100,

        [Description("已发货")] Printed = 105,

        [Description("发货完成")] PrintOver = 110,
    }
}