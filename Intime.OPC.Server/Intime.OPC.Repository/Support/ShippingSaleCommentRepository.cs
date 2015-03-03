// ***********************************************************************
// Assembly         : 01_Intime.OPC.Repository
// Author           : Liuyh
// Created          : 03-30-2014 11:40:56
//
// Last Modified By : Liuyh
// Last Modified On : 03-30-2014 11:42:28
// ***********************************************************************
// <copyright file="ShippingSaleCommentRepository.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;

namespace Intime.OPC.Repository.Support
{
    /// <summary>
    /// Class ShippingSaleCommentRepository.
    /// </summary>
    internal class ShippingSaleCommentRepository : BaseRepository<OPC_ShippingSaleComment>,
        IShippingSaleCommentRepository
    {
        #region IShippingSaleCommentRepository Members

        /// <summary>
        /// 通过快递单号获得备注
        /// </summary>
        /// <param name="shippingCode">The shipping code.</param>
        /// <returns>IList{OPC_ShippingSaleComment}.</returns>
        public IList<OPC_ShippingSaleComment> GetByShippingCode(string shippingCode)
        {
            return Select(t => t.ShippingCode == shippingCode).ToList();
        }

        #endregion
    }
}