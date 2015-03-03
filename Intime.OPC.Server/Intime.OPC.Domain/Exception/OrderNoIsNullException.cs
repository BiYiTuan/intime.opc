// ***********************************************************************
// Assembly         : 00_Intime.OPC.Domain
// Author           : Liuyh
// Created          : 03-26-2014 01:40:12
//
// Last Modified By : Liuyh
// Last Modified On : 03-26-2014 01:40:25
// ***********************************************************************
// <copyright file="OrderNoIsNullException.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Intime.OPC.Domain.Exception
{
    /// <summary>
    ///     Class OrderNoIsNullException.
    /// </summary>
    public class OrderNoIsNullException : OpcApiException
    {
        public OrderNoIsNullException(string orderNo)
            : base("订单编号为空")
        {
            this.OrderNo = orderNo;
        }

        public string OrderNo { get; private set; }
    }
}