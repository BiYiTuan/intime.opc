
using System.ComponentModel;

namespace Intime.OPC.Domain.Enums
{
    /// <summary>
    ///     退货状态
    /// </summary>
    public enum EnumReturnGoodsStatus
    {
        [Description("未处理")]
        NoProcess = 0,
        /// <summary>
        ///     The service approve
        /// </summary>
        [Description("客服批准")] ServiceApprove = 5,


        [Description("付款确认")]
        PayVerify = 7,

        /// <summary>
        ///     The compensate verify
        /// </summary>
        [Description("赔偿审核")] CompensateVerify = 10,

        /// <summary>
        ///     The compensate verify pass
        /// </summary>
        [Description("赔偿审核通过")] CompensateVerifyPass = 15,

        /// <summary>
        ///     The compensate verify failed
        /// </summary>
        [Description("赔偿审核未通过")] CompensateVerifyFailed =20,


        [Description("客服同意商品退回")]
        ServiceAgreeGoodsBack = 22,
        /// <summary>
        ///     The valid
        /// </summary>
        [Description("已生效")] Valid = 25
    }
}