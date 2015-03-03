using System.Collections.Generic;
using Intime.OPC.Domain.Enums;

namespace Intime.OPC.Domain.Dto.Request
{
    /// <summary>
    /// 销售单收银状态
    /// </summary>
    public class SalesOrderCashRequest
    {
        /// <summary>
        /// 销售单号
        /// </summary>
        public string SalesOrderNo { get; set; }

        /// <summary>
        /// 收银号
        /// </summary>
        public string CashNo { get; set; }

        /// <summary>
        /// 收银状态
        /// </summary>
        public EnumCashStatus? CashStatus { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 数据权限
        /// </summary>
        public List<int> DataRoleStores { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
