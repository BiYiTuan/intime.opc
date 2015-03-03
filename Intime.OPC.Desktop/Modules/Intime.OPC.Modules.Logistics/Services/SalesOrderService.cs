using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure.REST;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Infrastructure;

namespace Intime.OPC.Modules.Logistics.Services
{
    [Export(typeof(IService<OPC_Sale>))]
    [Export(typeof(ISalesOrderService))]
    public class SalesOrderService : ServiceBase<OPC_Sale>, ISalesOrderService
    {
        private IDeliveryOrderService _deliveryOrderService;
        private IService<Order> _orderService;
        private IService<Counter> _counterService;

        [ImportingConstructor]
        public SalesOrderService(IDeliveryOrderService deliveryOrderService, IService<Order> orderService, IService<Counter> counterService)
        {
            _deliveryOrderService = deliveryOrderService;
            _orderService = orderService;
            _counterService = counterService;
        }

        public override PagedResult<OPC_Sale> Query(IQueryCriteria queryCriteria)
        {
            var result = base.Query(queryCriteria);
            if (result.TotalCount > 0)
            {
                result.Data.ForEach(salesOrder => Compose(salesOrder));
            }

            return result;
        }

        public override IList<OPC_Sale> QueryAll(IQueryCriteria queryCriteria)
        {
            var salesOrders = base.QueryAll(queryCriteria);
            salesOrders.ForEach(salesOrder => Compose(salesOrder));

            return salesOrders;
        }

        public override OPC_Sale Query(int id)
        {
            return Query(id.ToString());
        }

        public override OPC_Sale Query(string uniqueID)
        {
            var salesOrder = base.Query(uniqueID);
            Compose(salesOrder);

            return salesOrder;
        }

        public void ReceivePayment(OPC_Sale salesOrder,string paymentNumber)
        {
            string uri = string.Format("{0}/{1}/cash", UriName, salesOrder.SaleOrderNo);
            Update(uri, new { CashNo = paymentNumber });
        }

        private void Compose(OPC_Sale salesOrder)
        {
            if (salesOrder.Order == null)
            { 
                salesOrder.Order = _orderService.Query(salesOrder.OrderNo);
            }
            if (salesOrder.Counter == null && salesOrder.SectionId.HasValue)
            { 
                salesOrder.Counter = _counterService.Query(salesOrder.SectionId.Value);
            }
            if (salesOrder.DeliveryOrder == null 
                && salesOrder.ShippingSaleId.HasValue 
                && salesOrder.ShippingSaleId.Value >0)
            { 
                salesOrder.DeliveryOrder = _deliveryOrderService.Query(salesOrder.ShippingSaleId.Value);
            }
        }
    }
}
