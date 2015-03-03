using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Intime.OPC.Domain.Customer
{
    /// <summary>
    /// 客服服务-退货提醒 查询订单
    /// </summary>
    public class OutOfStockNotifyRequest
    {
        public OutOfStockNotifyRequest()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string OrderNo { get; set; }

        public string SaleOrderNo { get; set; }

        public int? SaleOrderStatus { get; set; }

        public string PayType { get; set; }

        public int? StoreId { get; set; }
        public override string ToString()
        {
            return string.Format("StartDate={0}&EndDate={1}& OrderNo={2}&SaleOrderNo={3}&SaleOrderStatus={4}&PayType={5}&StoreId={6}&pageIndex={7}&pageSize={8}", StartDate, EndDate, OrderNo, SaleOrderNo, SaleOrderStatus, PayType, StoreId, 1, 300);
        }
    }
}
