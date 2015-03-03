using System;

namespace Intime.OPC.Domain.Customer
{
    /// <summary>
    ///     退货付款确认
    /// </summary>
    public class ReturnGoodsPayDto
    {
        public ReturnGoodsPayDto()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string OrderNo { get; set; }


        public string PayType { get; set; }

        public int? StoreId { get; set; }

        public override string ToString()
        {
            return
                string.Format(
                    "StartDate={0}&EndDate={1}&OrderNo={2}&StoreId={3}&PayType={4}&pageIndex={5}&pageSize={6}",
                    StartDate, EndDate, OrderNo, StoreId, PayType, 1, 300);
        }
    }
}