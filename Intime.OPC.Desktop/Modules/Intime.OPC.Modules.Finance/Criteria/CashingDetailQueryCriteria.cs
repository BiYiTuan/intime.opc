using System;
using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Domain.Attributes;

namespace Intime.OPC.Modules.Finance.Criteria
{
    public class CashingDetailQueryCriteria : QueryCriteria
    {
        public CashingDetailQueryCriteria()
        {
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            StoreId = -1;
        }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int? StoreId { get; set; }

        public string PayType { get; set; }

        public string FinancialType { get; set; }

        public string OrderNo { get; set; }

        public string SalesOrderNo { get; set; }

        public string OrderChannelNo { get; set; }
    }
}