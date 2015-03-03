using System;

namespace Intime.OPC.Domain.Customer
{
    /// <summary>
    ///     客户服务-客服退货查询-退货信息
    /// </summary>
    public class ReturnGoodsInfoGet
    {
        private DateTime _endDate = DateTime.Now;
        private DateTime _startDate = DateTime.Now;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        //退货单号
        public string RmaNo { get; set; }
        //订单号
        public string OrderNo { get; set; }
        //销售单号
        public string SaleOrderNo { get; set; }
        //退货状态
        public int? RmaStatus { get; set; }

        //支付方式
        public string PayType { get; set; }
        //门店id

        public int? StoreID { get; set; }


        public override string ToString()
        {
            return
                string.Format(
                    "OrderNo={0}&StartDate={1}&EndDate={2}&RmaNo={3}&SaleOrderNo={4}&RmaStatus={5}&PayType={6}&StoreID={7}&pageIndex={8}&pageSize={9}",
                    OrderNo, StartDate, EndDate, RmaNo, SaleOrderNo, RmaStatus, PayType, StoreID, 1, 300);
        }
    }
}