using System;
using Intime.OPC.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestService
{
    [TestClass]
    public class TestBrandService : TestService<IBrandService>
    {
        [TestMethod]
        public void TestGetAll()
        {
            
            var count = Service.GetAll();
            Assert.IsNotNull(count);
        }
    }
}
