using System;
using Intime.OPC.Infrastructure.Service;

namespace Intime.OPC.Modules.Finance.Criteria
{
    public class StatisticsQueryCrteria : QueryCriteria
    {
        public StatisticsQueryCrteria()
        {
            this.StartTime = DateTime.Now;
            this.EndTime = DateTime.Now;
            this.StoreId = -1;
        }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? StoreId { get; set; }
        public string OrderNo { get; set; }
        public string SalesOrderNo { get; set; }
        public string OrderChannelNo { get; set; }
    }
}