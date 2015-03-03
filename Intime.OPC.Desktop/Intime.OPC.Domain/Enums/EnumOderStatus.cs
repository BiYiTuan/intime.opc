// ***********************************************************************
// Assembly         : Intime.OPC.Domain
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
        /// <summary>
        ///     The no shipp
        /// </summary>
        [Description("未发货")] NoShipp = 0,

        /// <summary>
        ///     The shipped
        /// </summary>
        [Description("已发货")] Shipped = 5
    }
}