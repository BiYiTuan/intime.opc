using Intime.OPC.Service.Contract;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestService
{
    [TestClass]
    public class SalesOrderServiceTests : TestService<ISalesOrderService>
    {
        [TestMethod]
        public void GetSaleOrderDetails()
        {
            var details = Service.GetSaleDetails("114042236511-001");

            Assert.IsNotNull(details);
        }
    }
}
