using System.Collections.Generic;

namespace Intime.OPC.Domain.Dto.Request
{
    /// <summary>
    /// 合伙人 佣金 报表 请求
    /// </summary>
    public class AssociateCommissionStatisticsReportRequest : PageRequest, IStoreDataRoleRequest
    {
        public int? StoreId { get; set; }
        public List<int> DataRoleStores { get; set; }
        public int? CurrentUserId { get; set; }

        /// <summary>
        /// 专柜码
        /// </summary>
        public string SectionCode { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 迷你银帐号
        /// </summary>
        public string MiniSilverNo { get; set; }
    }
}