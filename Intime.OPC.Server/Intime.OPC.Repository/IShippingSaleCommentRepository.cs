// ***********************************************************************
// Assembly         : 01_Intime.OPC.Repository
// Author           : Liuyh
// Created          : 03-30-2014 11:38:10
//
// Last Modified By : Liuyh
// Last Modified On : 03-30-2014 11:38:28
// ***********************************************************************
// <copyright file="IShippingSaleCommentRepository.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository
{
    /// <summary>
    ///     Interface IShippingSaleCommentRepository
    /// </summary>
    public interface IShippingSaleCommentRepository : IRepository<OPC_ShippingSaleComment>
    {
        /// <summary>
        /// 通过快递单号获得备注
        /// </summary>
        /// <param name="shippingCode">The shipping code.</param>
        /// <returns>IList{OPC_ShippingSaleComment}.</returns>
        IList<OPC_ShippingSaleComment> GetByShippingCode(string shippingCode);
    }
}