// ***********************************************************************
// Assembly         : 00_Intime.OPC.Domain
// Author           : Liuyh
// Created          : 04-01-2014 23:22:49
//
// Last Modified By : Liuyh
// Last Modified On : 04-01-2014 23:25:09
// ***********************************************************************
// <copyright file="EnumOrgType.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;

namespace Intime.OPC.Domain.Enums
{
    /// <summary>
    /// 组织机构枚举
    /// </summary>
    public enum EnumOrgType
    {
        /// <summary>
        /// The org
        /// </summary>
        [Description("组织机构")] Org = 0,
        /// <summary>
        /// The store
        /// </summary>
        [Description("门店")] Store = 5,
        /// <summary>
        /// The section
        /// </summary>
        [Description("专柜")] Section = 10
    }
}