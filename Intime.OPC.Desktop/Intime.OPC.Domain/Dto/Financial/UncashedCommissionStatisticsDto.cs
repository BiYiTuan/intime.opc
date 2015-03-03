using Intime.OPC.Domain.Attributes;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Domain.Dto.Financial
{
    /// <summary>
    /// 未提取导购佣金统计报表
    /// </summary>
    [Uri("statistics/associatecommissionreport")]
    public class UncashedCommissionStatisticsDto : Model
    {
        /// <summary>
        /// Store ID
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
        /// 联系方式
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 迷你银帐号
        /// </summary>
        public string MiniSilverNo { get; set; }

        /// <summary>
        /// 未提取金额
        /// </summary>
        public decimal? NoPickUpAmount { get; set; }

        /// <summary>
        /// 已取金额
        /// </summary>
        public decimal? HavePickUpAmount { get; set; }

        /// <summary>
        /// 不可提取金额
        /// </summary>
        public decimal? LockedPickUpAmount { get; set; }

        /// <summary>
        /// 申请中的提取金额
        /// </summary>
        public decimal? ApplicationPickUpAmount { get; set; }
    }
}
