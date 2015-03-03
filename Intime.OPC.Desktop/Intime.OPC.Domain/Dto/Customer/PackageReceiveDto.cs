using System;

namespace Intime.OPC.Domain.Customer
{
    //退货包裹签收确认
    public class PackageReceiveDto
    {
        public PackageReceiveDto()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string OrderNo { get; set; }

        public string SaleOrderNo { get; set; }

        public override string ToString()
        {
            return string.Format("StartDate={0}&EndDate={1}&OrderNo={2}&SaleOrderNo={3}&pageIndex={4}&pageSize={5}",
                StartDate, EndDate, OrderNo, SaleOrderNo, 1, 300);
        }
    }
}