using System;

namespace Intime.OPC.Domain.Models
{
    public class OrderGet
    {
        public OrderGet()
        {
            StartCreateDate = DateTime.Now;
            EndCreateDate = DateTime.Now;
            Status = "-1";
            ExpressDeliveryCompany = "-1";
        }

        public string OrderNo { get; set; }
        public string OrderSource { get; set; }
        public string StoreId { get; set; }
        public string BrandId { get; set; }
        public string Status { get; set; }

        public string PaymentType { get; set; }
        public string OutGoodsType { get; set; }
        public string ShippingContactPhone { get; set; }
        public string ExpressDeliveryCode { get; set; }
        public string ExpressDeliveryCompany { get; set; }

        public DateTime StartCreateDate { get; set; }
        //开始时间条件

        public DateTime EndCreateDate { get; set; } //结束时间条件
    }
}