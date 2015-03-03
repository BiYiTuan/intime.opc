// ***********************************************************************
// Assembly         : 00_Intime.OPC.Domain
// Author           : Liuyh
// Created          : 03-25-2014 02:12:44
//
// Last Modified By : Liuyh
// Last Modified On : 04-05-2014 17:14:49
// ***********************************************************************
// <copyright file="EnumRMAStatus.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel;

namespace Intime.OPC.Domain.Enums
{
    /// <summary>
    ///     退货单状态
    /// </summary>
    public enum EnumRMAStatus
    {
        /// <summary>
        /// 作废状态
        /// </summary>
        [Description("作废")]
        Void = -1,
        /// <summary>
        ///     未收货
        /// </summary>
        [Description("未收货")] NoDelivery = 0,

        /// <summary>
        ///     物流未收货
        /// </summary>
        [Description("物流未收货")] ShipNoReceive = 2,

        /// <summary>
        ///     物流收货
        /// </summary>
        [Description("物流收货")] ShipReceive = 5,

        ///// <summary>
        /////     物流审核
        ///// </summary>
        //[Description("物流审核")] ShipVerify = 10,

        /// <summary>
        ///     物流审核未通过
        /// </summary>
        [Description("物流审核未通过")] ShipVerifyNotPass = 15,

        /// <summary>
        ///     物流审核通过
        /// </summary>
        [Description("物流审核通过")] ShipVerifyPass = 20,

        /// <summary>
        ///     物流入库
        /// </summary>
        [Description("物流入库")] ShipInStorage = 25,

        [Description("通知单品")]
        NotifyProduct = 26,

        [Description("付款确认")]
        PayVerify = 28,

        /// <summary>
        ///     打印退货单
        /// </summary>
        [Description("完成打印退货单")] PrintRMA = 30,

        /// <summary>
        ///     导购确认收货
        /// </summary>
        [Description("导购确认收货")] ShoppingGuideReceive = 35,

        /// <summary>
        ///     未收货
        /// </summary>
        [Description("缺货")]OutofStack = 999
    }
}