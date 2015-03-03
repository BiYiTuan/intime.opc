using System;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Converters;

namespace TestService
{
    [TestClass]
    public class TestEnumService : TestService<IEnumService>
    {

        [TestMethod]
        public void TestGetEnum()
        {
            var lst=  Service.All(typeof (EnumOderStatus));
            Assert.IsNotNull(lst);
        }
    }
}
