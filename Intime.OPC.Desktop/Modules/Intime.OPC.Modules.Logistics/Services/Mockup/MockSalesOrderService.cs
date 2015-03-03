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

namespace Intime.OPC.Modules.Logistics.Services
{
    //[Export(typeof(IService<OPC_Sale>))]
    public class MockSalesOrderService : ServiceBase<OPC_Sale>
    {
        private Fixture fixture = new Fixture();

        public override PagedResult<OPC_Sale> Query(IQueryCriteria queryCriteria)
        {
            var salesOrders = fixture.Build<OPC_Sale>()
                .Without(so => so.DeliveryOrder)
                .Without(so => so.Counter)
                .Do(so => so.IsSelected = false)
                .Do(so => so.Status = EnumSaleOrderStatus.ShipInStorage)
                .CreateMany(200);

            var result = new PagedResult<OPC_Sale>() { PageIndex = queryCriteria.PageIndex, PageSize = queryCriteria.PageSize, TotalCount = 200, Data = salesOrders.ToList() };

            return result;
        }

        public override IList<OPC_Sale> QueryAll(IQueryCriteria queryCriteria)
        {
            var result = Query(queryCriteria);
            return result.Data;
        }

        public override OPC_Sale Update(OPC_Sale obj)
        {
            return obj;
        }
    }
}
