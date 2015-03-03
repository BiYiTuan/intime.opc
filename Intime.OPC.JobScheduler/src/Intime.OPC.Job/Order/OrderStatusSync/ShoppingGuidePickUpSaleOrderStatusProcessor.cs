using System;
using System.Linq;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Order.DTO;

namespace Intime.OPC.Job.Order.OrderStatusSync
{
    public class ShoppingGuidePickUpSaleOrderStatusProcessor : AbstractSaleOrderStatusProcessor
    {
        public ShoppingGuidePickUpSaleOrderStatusProcessor(EnumSaleOrderStatus status) : base(status) { }
        public override void Process(string saleOrderNo, OrderStatusResultDto statusResult)
        {
            using (var db = new YintaiHZhouContext())
            {
                var saleOrder = db.OPC_Sale.FirstOrDefault(o => o.SaleOrderNo == saleOrderNo);
                if (saleOrder.Status != (int)EnumSaleOrderStatus.PrintSale) return;
                saleOrder.Status = (int)_status;
                saleOrder.UpdatedDate = DateTime.Now;
                saleOrder.UpdatedUser = -100;
                db.SaveChanges();
            }
        }
    }
}