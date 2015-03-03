using System;

namespace Intime.OPC.Domain.Dto
{
    /// <summary>
    /// 已经兑现 合伙人 佣金 报表
    /// </summary>
    public class CashAssociateCommissionStatisticsReportDto
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public int SectionId { get; set; }

        public string SectionCode { get; set; }

        public string SectionName { get; set; }

        /// <summary>
        /// 迷你银帐号
        /// </summary>
        public string MiniSilverNo { get; set; }

        /// <summary>
        /// 提取日期
        /// </summary>
        public DateTime? PickUpDate { get; set; }

        /// <summary>
        /// 提取金额
        /// </summary>
        public decimal? PickUpAmount { get; set; }

        /// <summary>
        /// 提取人
        /// </summary>
        public string PickUpPerson { get; set; }

        ///// <summary>
        ///// 支付订单号
        ///// </summary>
        //public string PaymentOrderNo { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        public string BankCardNo { get; set; }

        /// <summary>
        /// 手续费 目前固定1元
        /// </summary>
        public decimal? Fee
        {
            get
            {
                return 1;
            }
            set { }
        }

        /// <summary>
        /// 税金
        /// </summary>
        public decimal? Taxes { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string Contact { get; set; }
    }
}