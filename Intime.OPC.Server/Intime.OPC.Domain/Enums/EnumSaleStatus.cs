// ***********************************************************************
// Assembly         : OPCApp.Domain
// Author           : Liuyh
// Created          : 03-20-2014 22:40:53
//
// Last Modified By : Liuyh
// Last Modified On : 03-20-2014 22:47:30
// ***********************************************************************
// <copyright file="EnumSaleStatue.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

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