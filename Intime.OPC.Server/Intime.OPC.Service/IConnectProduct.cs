// ***********************************************************************
// Assembly         : 02_Intime.OPC.Service
// Author           : Liuyh
// Created          : 04-13-2014 18:52:05
//
// Last Modified By : Liuyh
// Last Modified On : 04-13-2014 18:53:51
// ***********************************************************************
// <copyright file="IConnectProduct.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Intime.OPC.Service
{
    /// <summary>
    /// Interface IConnectProduct
    /// </summary>
    public interface IConnectProduct
    {
        /// <summary>
        /// 获取收银流水号
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <param name="rmaNo">The rma no.</param>
        /// <param name="money">The money.</param>
        /// <returns>System.String.</returns>
        string GetCashNo(string orderNo, string rmaNo, decimal money);
    }
}