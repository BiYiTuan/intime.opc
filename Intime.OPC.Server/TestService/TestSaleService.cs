using System;
using Intime.OPC.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestService
{
    [TestClass]
    public class TestSaleService:TestService<ISaleService>
    {
        [TestMethod]
        public void TestGetSaleDetail()
        {
            var lst = Service.GetSaleOrderDetails("114042236511-001", 1, 1, 1000);
           Debug(lst);
            Assert.IsNotNull(lst);
            
        }


        [TestMethod]
        public void TestGetSaleByOrderNo()
        {
            var lst = Service.GetByOrderNo("114042236511", 1, 1, 1000);
            Debug(lst);
            Assert.IsNotNull(lst);

        }

        [TestMethod]
        public void TestGetNoPickUp()
        {
            var lst = Service.GetNoPickUp("", 1, "", new DateTime(2000, 1, 1), DateTime.Now.Date, 1, 50);
            Debug(lst);
            Assert.IsNotNull(lst);

        }

        [TestMethod]
        public void TestGetPrintSale()
        {
            var lst = Service.GetPrintSale("", 1, "", new DateTime(2000, 1, 1), DateTime.Now.Date, 1, 50);
            Debug(lst);
            Assert.IsNotNull(lst);

        }
        //
    }
}
