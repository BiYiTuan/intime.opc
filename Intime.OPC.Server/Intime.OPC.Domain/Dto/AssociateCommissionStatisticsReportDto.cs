namespace Intime.OPC.Domain.Dto
{
    /// <summary>
    /// 合伙人 佣金 报表
    /// </summary>
    public class AssociateCommissionStatisticsReportDto
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public int SectionId { get; set; }

        public string SectionCode { get; set; }

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