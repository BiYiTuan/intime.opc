using System.ComponentModel;

namespace Intime.OPC.Domain.Enums
{
    /// <summary>
    ///     销售单状态
    /// </summary>
    public enum EnumSaleOrderStatus
    {
        /// <summary>
        ///     无
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        ///     未提货
        /// </summary>
        [Description("未提货")]
        NoPickUp = 0,

        /// <summary>
        ///     通知单品
        /// </summary>
        [Description("通知单品")]
        NotifyProduct = 1,

        /// <summary>
        ///     完成销售单打印
        /// </summary>
        [Description("完成销售单打印")]
        PrintSale = 2,

        /// <summary>
        ///     商品已被专柜导购的单品系统获取到
        /// </summary>
        [Description("商品已被专柜导购的单品系统获取到")]
        Fetched = 21,

        /// <summary>
        ///     缺货
        /// </summary>
        [Description("缺货")]
        StockOut = 5,

        /// <summary>
        ///     已补货
        /// </summary>
        [Description("已补货")]
        Replenished = 10,

        /// <summary>
        ///     已提货
        /// </summary>
        [Description("已提货")]
        PickUp = 15,

        /// <summary>
        ///     导购提货确认
        /// </summary>
        [Description("导购提货确认")]
        ShoppingGuidePickUp = 20,

        /// <summary>
        ///     物流入库
        /// </summary>
        [Description("物流入库")]
        ShipInStorage = 25,

        /// <summary>
        ///     已生成发货单
        /// </summary>
        [Description("已生成发货单")]
        PrintInvoice = 30,

        /// <summary>
        ///     已生成快递单
        /// </summary>
        [Description("已生成快递单")]
        PrintExpress = 35,

        /// <summary>
        ///     已发货
        /// </summary>
        [Description("已发货")]
        Shipped = 40,

        /// <summary>
        ///     订单完成
        /// </summary>
        [Description("订单完成")]
        SaleCompletion = 900,

        [Description("取消")]
        Void = -10
    }
}