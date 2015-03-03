using System;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Dto.Financial
{
    /// <summary>
    /// 网上收银流水对账查询
    /// </summary>
    public class SearchCashierRequest : SearchStatRequest
    {
        public SearchCashierRequest()
            : base(2000)
        {
        }


        public SearchCashierRequest(int maxPageSize = 2000)
            : base(maxPageSize)
        {
        }

        public string PayType { get; set; }

        /// <summary>
        /// RMA SALES
        /// </summary>
        public string FinancialType { get; set; }
    }
}