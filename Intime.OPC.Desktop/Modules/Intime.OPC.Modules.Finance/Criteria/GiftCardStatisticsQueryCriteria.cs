using System;
using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Domain.Attributes;

namespace Intime.OPC.Modules.Finance.Criteria
{
    public class GiftCardStatisticsQueryCriteria : QueryCriteria
    {
        public GiftCardStatisticsQueryCriteria()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            StoreId = -1;
        }

        [UriParameter("storeid")]
        public int StoreId { get; set; }

        [UriParameter("BuyStartDate")]
        public DateTime StartDate { get; set; }

        [UriParameter("BuyEndDate")]
        public DateTime EndDate { get; set; }

        [UriParameter("TransNo")]
        public string ChannelNo { get; set; }

        [UriParameter("GiftCardNo")]
        public string GiftCardNo { get; set; }

        [UriParameter("PaymentMethodCode")]
        public string Payment { get; set; }
    }
}
