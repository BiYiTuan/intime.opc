// ***********************************************************************
// Assembly         : 00_Intime.OPC.Domain
// Author           : Liuyh
// Created          : 03-26-2014 01:35:01
//
// Last Modified By : Liuyh
// Last Modified On : 03-26-2014 01:35:45
// ***********************************************************************
// <copyright file="OrderNotExistsException.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Data;

namespace Intime.OPC.Domain.Exception
{
    /// <summary>
    ///     Class OrderNotExistsException.
    /// </summary>
    public class OrderNotExistsException : OpcApiException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OrderNotExistsException" /> class.
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        public OrderNotExistsException(string orderNo)
            : base(string.Format("订单不存在,订单Id：{0}",orderNo))
        {
            this.OrderNo = orderNo;
        }

        public string OrderNo { get; private set; }
    }
}