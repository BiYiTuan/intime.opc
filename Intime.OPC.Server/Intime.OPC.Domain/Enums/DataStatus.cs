using System.ComponentModel;

namespace Intime.OPC.Domain.Enums
{
    /// <summary>
    /// 数据状态枚举，与数据库status 要保持一致，不允许修改 值
    /// copy https://github.com/victorgong/ytoo.service/blob/july/Yintai.Hangzhou.Model/Enums/DataStatus.cs
    /// </summary>
    public enum DataStatus
    {
        /// <summary>
        /// 已删除（逻辑删除）
        /// </summary>
        [Description("删除")]
        Deleted = -1,
        /// <summary>
        /// 默认状态
        /// </summary>
        [Description("未上架")]
        Default = 0,
        /// <summary>
        /// 正常状态
        /// </summary>
        [Description("已上架")]
        Normal = 1,

        //#region  业务状态
        //// 业务状态 1000起

        ////优惠券1001~1100

        ///// <summary>
        ///// 已使用 优惠券
        ///// </summary>
        //CouponUse = 1001



        //#endregion


    }
}