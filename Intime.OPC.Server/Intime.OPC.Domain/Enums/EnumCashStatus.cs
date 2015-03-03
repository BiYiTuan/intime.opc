// ***********************************************************************
// Assembly         : OPCApp.Domain
// Author           : Liuyh
// Created          : 03-20-2014 23:17:15
//
// Last Modified By : Liuyh
// Last Modified On : 03-20-2014 23:19:40
// ***********************************************************************
// <copyright file="EnumCashStatus.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel;

namespace Intime.OPC.Domain.Enums
{
    /// <summary>
    ///     收银状态
    /// </summary>
    public enum EnumCashStatus
    {
        /// <summary>
        /// 未送收银
        /// </summary>
        [Description("未送收银")]
        NoCash = 0,
        /// <summary>
        /// 收银失败
        /// </summary>
        [Description("送收银失败")]
        CashingFailed = 1,

        /// <summary>
        /// 已送收银
        /// </summary>
        [Description("已送收银")]
        SendCash = 5,

        /// <summary>
        /// 送收银后轮询失败
        /// </summary>
        [Description("送收银失败.")]
        CashedButFailed = 6,

        /// <summary>
        /// 完成收银
        /// </summary>
        [Description("完成收银")]
        CashOver = 10
    }
}