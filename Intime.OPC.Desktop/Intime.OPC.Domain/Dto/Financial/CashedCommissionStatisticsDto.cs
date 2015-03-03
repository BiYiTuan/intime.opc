using System;
using Intime.OPC.Domain.Attributes;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Domain.Dto.Financial
{
    /// <summary>
    /// 已经兑现佣金统计报表
    /// </summary>
     [Uri("statistics/cashassociatecommissionreport")]
    public class CashedCommissionStatisticsDto : Model
    {
        /// <summary>
        /// Store Id
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Store Name
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// Section Id
        /// </summary>
        public int SectionId { get; set; }

        /// <summary>
        /// Section Code
        /// </summary>
        public string SectionCode { get; set; }

        /// <summary>
        /// Section Name
        /// </summary>
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

        /// <summary>
        /// 导购联系方式
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        public string BankCardNo { get; set; }

        /// <summary>
        /// 手续费
        /// </summary>
        public decimal? Fee { get; set; }

        /// <summary>
        /// 税金
        /// </summary>
        public decimal? Taxes { get; set; }
    }
}
