using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Order.DTO;
using System;
using System.Linq;

namespace Intime.OPC.Job.RMASync
{
    public class ShoppingGuidePickUpRMASaleStatusProcessor : AbstractRMASaleStatusProcessor
    {
        public ShoppingGuidePickUpRMASaleStatusProcessor(EnumRMAStatus status) : base(status) { }
        public override void Process(string rmaNo, OrderStatusResultDto statusResult)
        {
            using (var db = new YintaiHZhouContext())
            {
                var saleRMA = db.OPC_RMA.FirstOrDefault(o => o.RMANo == rmaNo);
                if (saleRMA.Status == (int)EnumRMAStatus.ShoppingGuideReceive || saleRMA.Status == (int)EnumRMAStatus.PayVerify) return;
                saleRMA.Status = (int)EnumRMAStatus.ShoppingGuideReceive;
                saleRMA.UpdatedDate = DateTime.Now;
                saleRMA.UpdatedUser = -100;
                db.SaveChanges();
            }
        }
    }
}
