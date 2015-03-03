using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Dto.Request
{
    /// <summary>
    /// 已经兑现合伙人 佣金 报表 请求
    /// </summary>
    public class CashAssociateCommissionStatisticsReportRequest : PageRequest, IStoreDataRoleRequest
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

        /// <summary>
        /// 提现 开始 日期
        /// </summary>
        public DateTime? PickUpStartDate { get; set; }

        /// <summary>
        /// 提现 结束 日期
        /// </summary>
        public DateTime? PickUpEndDate { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 银行CODE
        /// </summary>
        public string BankCode { get; set; }

        public override void ArrangeParams()
        {
            if (PickUpStartDate != null)
            {
                PickUpStartDate = PickUpStartDate.Value.Date;
            }

            if (PickUpEndDate != null)
            {
                PickUpEndDate = PickUpEndDate.Value.Date.AddDays(1);
            }

            base.ArrangeParams();
        }
    }
}
