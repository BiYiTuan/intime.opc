// ***********************************************************************
// Assembly         : Intime.OPC.Domain
// Author           : Liuyh
// Created          : 03-20-2014 22:49:04
//
// Last Modified By : Liuyh
// Last Modified On : 03-20-2014 23:09:08
// ***********************************************************************
// <copyright file="EnumSaleOrderStatue.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel;

namespace Intime.OPC.Domain.Enums
{
    /// <summary>
    ///     销售单状态
    /// </summary>
    public enum EnumSaleOrderStatus
    {
        /// <summary>
        ///     The no pick up
        /// </summary>
        [Description("未提货")] NoPickUp = 0,

        /// <summary>
        ///     通知单品
        /// </summary>
        [Description("通知单品")] NotifyProduct = 1,

        /// <summary>
        ///     打印销售单
        /// </summary>
        [Description("打印销售单")] PrintSale = 2,
        /// <summary>
        ///     The stock out
        /// </summary>
        [Description("缺货")] StockOut = 5,

        /// <summary>
        ///     The replenished
        /// </summary>
        [Description("已补货")] Replenished = 10,

        /// <summary>
        ///     The pick up
        /// </summary>
        [Description("已提货")] PickUp = 15,

        /// <summary>
        ///     The shopping guide pick up
        /// </summary>
        [Description("导购提货")] ShoppingGuidePickUp = 20,

        /// <summary>
        ///     The put in storage
        /// </summary>
        [Description("物流入库")] ShipInStorage = 25,

        /// <summary>
        ///     The print invoice
        /// </summary>
        [Description("已生成发货单")] PrintInvoice = 30,

        /// <summary>
        ///     The print express
        /// </summary>
        [Description("已生成快递单")] PrintExpress = 35,

        /// <summary>
        ///     The shipped
        /// </summary>
        [Description("已发货")] Shipped = 40,
    }
}