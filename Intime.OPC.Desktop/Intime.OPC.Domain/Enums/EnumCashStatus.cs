// ***********************************************************************
// Assembly         : Intime.OPC.Domain
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
        ///     The no shipp
        /// </summary>
        [Description("未送收银")] NoCash = 0,

        /// <summary>
        ///     The shipped
        /// </summary>
        [Description("完成收银")] CashOver = 5
    }
}