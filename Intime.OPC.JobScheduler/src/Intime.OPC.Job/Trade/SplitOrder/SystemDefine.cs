
namespace Intime.OPC.Job.Trade.SplitOrder
{
    public class SystemDefine
    {
        /// <summary>
        /// OPC系统拆单操作者Id
        /// </summary>
        public const int SysUserId = -999;

        /// <summary>
        /// 销售单默认状态
        /// </summary>
        public const int SaleOrderDefaultStatus = 0;

        /// <summary>
        /// 完成拆单状态
        /// </summary>
        public const int OrderFinishSplitStatusCode = 0;

        /// <summary>
        /// 订单拆单库存错误状态
        /// </summary>
        public const int OrderSplitStockInvalid = 9500;

        /// <summary>
        /// 订单拆单异常状态
        /// </summary>
        public const int OrderSplitException = 9501;
    }
}
