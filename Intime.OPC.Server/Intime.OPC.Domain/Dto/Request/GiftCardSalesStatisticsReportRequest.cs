using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Dto.Request
{
    /// <summary>
    /// 礼品卡统计报表请求
    /// </summary>
    public class GiftCardSalesStatisticsReportRequest : PageRequest, IStoreDataRoleRequest
    {
        public int? StoreId { get; set; }
        public List<int> DataRoleStores { get; set; }
        public int? CurrentUserId { get; set; }

        public DateTime? BuyStartDate { get; set; }

        public DateTime? BuyEndDate { get; set; }

        /// <summary>
        /// 礼品卡 no
        /// </summary>
        public string GiftCardNo { get; set; }

        /// <summary>
        /// 渠道订单号
        /// </summary>
        public string TransNo { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string PaymentMethodCode { get; set; }

        public override void ArrangeParams()
        {

            base.ArrangeParams();

            if (BuyStartDate != null)
            {
                BuyStartDate = BuyStartDate.Value.Date;
            }

            if (BuyEndDate != null)
            {
                BuyEndDate = BuyEndDate.Value.Date.AddDays(1);
            }
        }
    }
}