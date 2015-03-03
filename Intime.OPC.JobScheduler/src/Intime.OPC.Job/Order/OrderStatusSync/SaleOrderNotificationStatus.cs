namespace Intime.OPC.Job.Order.OrderStatusSync
{
    public enum SaleOrderNotificationStatus
    {
        /// <summary>
        /// 抛出异常的错误
        /// </summary>
        ExceptionThrow = 0,

        /// <summary>
        /// 创建订单
        /// </summary>
        Create = 1,

        /// <summary>
        /// 创建订单失败
        /// </summary>
        CreateFailed = 2,

        /// <summary>
        /// 支付
        /// </summary>
        Paid = 3,

        /// <summary>
        /// 支付失败
        /// </summary>
        PaidFailed = 4,

    }
}