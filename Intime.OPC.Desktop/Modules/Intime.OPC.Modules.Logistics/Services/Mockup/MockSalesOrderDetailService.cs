using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure.REST;
using Ploeh.AutoFixture;
using System.Linq;

namespace Intime.OPC.Modules.Logistics.Services
{
    //[Export(typeof(IService<OPC_SaleDetail>))]
    public class MockSalesOrderDetailService : ServiceBase<OPC_SaleDetail>
    {
        private Fixture fixture = new Fixture();

        public override PagedResult<OPC_SaleDetail> Query(IQueryCriteria queryCriteria)
        {
            var salesOrderDetails = fixture.Build<OPC_SaleDetail>().CreateMany(5);

            var result = new PagedResult<OPC_SaleDetail>() { PageIndex = queryCriteria.PageIndex, PageSize = queryCriteria.PageSize, TotalCount = 200, Data = salesOrderDetails.ToList() };

            return result;
        }
    }
}
