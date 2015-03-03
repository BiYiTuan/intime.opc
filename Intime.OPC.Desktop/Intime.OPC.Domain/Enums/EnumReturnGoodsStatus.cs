// ***********************************************************************
// Assembly         : Intime.OPC.Domain
// Author           : Liuyh
// Created          : 03-20-2014 23:23:27
//
// Last Modified By : Liuyh
// Last Modified On : 03-20-2014 23:29:55
// ***********************************************************************
// <copyright file="EnumReturnGoodsStatus.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel;

namespace Intime.OPC.Domain.Enums
{
    /// <summary>
    ///     退货状态
    /// </summary>
    public enum EnumReturnGoodsStatus
    {
        /// <summary>
        ///     The service approve
        /// </summary>
        [Description("客服批准")] ServiceApprove = 0,


        /// <summary>
        ///     The compensate verify
        /// </summary>
        [Description("赔偿审核")] CompensateVerify = 5,


        /// <summary>
        ///     The compensate verify pass
        /// </summary>
        [Description("赔偿审核通过")] CompensateVerifyPass = 5,


        /// <summary>
        ///     The compensate verify failed
        /// </summary>
        [Description("赔偿审核未通过")] CompensateVerifyFailed = 5,

        /// <summary>
        ///     The valid
        /// </summary>
        [Description("已生效")] Valid = 5
    }
}