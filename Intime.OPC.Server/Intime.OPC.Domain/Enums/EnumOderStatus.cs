// ***********************************************************************
// Assembly         : OPCApp.Domain
// Author           : Liuyh
// Created          : 03-20-2014 23:17:15
//
// Last Modified By : Liuyh
// Last Modified On : 03-20-2014 23:19:40
// ***********************************************************************
// <copyright file="OderStatus.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel;

namespace Intime.OPC.Domain.Enums
{
    /// <summary>
    ///     订单状态
    /// </summary>
    public enum EnumOderStatus
    {
        ///// <summary>
        /////     The no shipp
        ///// </summary>
        //[Description("未发货")] NoShipp = 0,

        ///// <summary>
        /////     The shipped
        ///// </summary>
        //[Description("已发货")] Shipped = 5

        [Description("已创建")] Create = 0,
        [Description("取消")] Void = -10,
        [Description("已付款")] Paid = 1,
        [Description("审核通过")] PassConfirmed = 2,
        [Description("专柜已修改")] AgentConfirmed = 11,
        [Description("用户确认")] CustomerConfirmed = 12,
        [Description("订单已打印")] OrderPrinted = 13,
        [Description("发货单已打印")] PreparePack = 14,
        [Description("已发货")] Shipped = 15,
        [Description("用户已签收")] CustomerReceived = 16,
        [Description("转集团销售")] Convert2Sales = 17,
        [Description("完成")] Complete = 18,
        [Description("全部退货")] RMAd = 19,
        [Description("用户拒收")] CustomerRejected = 10,
        [Description("库存不足")]
        StockOut = 9500
        
    }
}