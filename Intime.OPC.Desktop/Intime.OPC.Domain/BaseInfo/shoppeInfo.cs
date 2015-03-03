// ***********************************************************************
// Assembly         : OPCAPP.Domain
// Author           : Liuyh
// Created          : 03-10-2014 21:10:22
//
// Last Modified By : Liuyh
// Last Modified On : 03-10-2014 21:11:13
// ***********************************************************************
// <copyright file="ShoppeInfo.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Intime.OPC.Domain.BaseInfo
{
    /// <summary>
    ///     专柜信息
    /// </summary>
    public class ShoppeInfo
    {
        /// <summary>
        ///     专柜ID
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        ///     专柜名称
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
    }
}