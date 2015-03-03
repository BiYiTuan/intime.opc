using System;
using Intime.OPC.Domain.Attributes;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Domain.Dto.Financial
{
    /// <summary>
    /// 礼品卡统计报表
    /// </summary>
    [Uri("statistics/giftcardsalesreport")]
    public class GiftCardStatisticsDto : Model
    {
        /// <summary>
        /// 礼品卡Number
        /// </summary>
        public string GiftCardNo { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 渠道订单号
        /// </summary>
        public string TransNo { get; set; }

        /// <summary>
        /// 付款方式代码
        /// </summary>
        public string PaymentMethodCode { get; set; }

        /// <summary>
        /// 付款方式名称
        /// </summary>
        public string PaymentMethodName { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 购买时间
        /// </summary>
        public DateTime? BuyDate { get; set; }

        /// <summary>
        /// 礼品卡金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 礼品卡销售金额
        /// </summary>
        public decimal? SalesAmount { get; set; }

        /// <summary>
        /// 状态 是否充值? 1 否 : 是
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 充值
        /// </summary>
        public string Recharge { get; set; }
    }
}
