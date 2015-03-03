using System;

namespace Intime.OPC.Domain.Dto
{
    /// <summary>
    /// 礼品卡 统计报表
    /// </summary>
    public class GiftCardSalesStatisticsReportDto
    {
        //public int GiftCardItemId { get; set; }

        //public int GiftCardId { get; set; }

        //public string GiftCardName { get; set; }

        public int Id { get; set; }

        /// <summary>
        /// 礼品卡 No
        /// </summary>
        public string GiftCardNo
        {
            get { return OrderNo; }
            set { }
        }

        public string OrderNo { get; set; }

        public string TransNo { get; set; }

        public string PaymentMethodCode { get; set; }

        public string PaymentMethodName { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public DateTime? BuyDate { get; set; }

        /// <summary>
        /// 礼品卡金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 销售金额
        /// </summary>
        public decimal? SalesAmount { get; set; }

        /// <summary>
        /// 状态 是否充值? 1 否 : 是
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 充值
        /// </summary>
        public string Recharge
        {
            get { return Status == 1 ? "否" : "是"; }
            set { }
        }
    }
}