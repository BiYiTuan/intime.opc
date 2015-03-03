// ***********************************************************************
// Assembly         : Intime.OPC.Domain
// Author           : Hyx
// Created          : 03-23-2014
//
// Last Modified By : Hyx
// Last Modified On : 03-24-2014
// ***********************************************************************
// <copyright file="EnumSearchSaleStatus.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

/// <summary>
/// The Enums namespace.
/// </summary>

namespace Intime.OPC.Domain.Enums
{
    /// <summary>
    ///     收银状态
    ///     <summary>
    ///         Enum EnumSearchSaleStatus
    ///     </summary>
    public enum EnumSetRemarkType
    {
        /// <summary>
        ///     填写销售单备注类型
        /// </summary>
        SetSaleRemark = 0,

        /// <summary>
        ///     填写销售单明细备注类型
        /// </summary>
        SetSaleDetailRemark = 1,

        /// <summary>
        ///     填写订单备注类型
        /// </summary>
        SetOrderRemark = 2,

        /// <summary>
        ///     填写快递单备注
        /// </summary>
        SetShipSaleRemark = 3,

        /// <summary>
        ///     填写退货单备注
        /// </summary>
        SetSaleRMARemark = 4,

        /// <summary>
        ///     填写退货单备注
        /// </summary>
        SetRMARemark = 5
    }
}