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
    public enum EnumSearchSaleStatus
    {
        /// <summary>
        ///     完成打印查询
        /// </summary>
        CompletePrintSearchStatus = 0,

        /// <summary>
        ///     入库查询
        /// </summary>
        StoreInDataBaseSearchStatus = 5,

        /// <summary>
        ///     出库 打印发货但查询
        /// </summary>
        StoreOutDataBaseSearchStatus = 10,

        /// <summary>
        ///     打印发货单查询
        /// </summary>
        PrintInvoiceSearchStatus = 15,

        /// <summary>
        ///     打印快递单查询
        /// </summary>
        PrintExpressSearchStatus = 20,
        ShipedSearchStatus = 30
    }
}