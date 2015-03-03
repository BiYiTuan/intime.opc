using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestService
{
    
    [TestClass]
    public class TestMenuService : TestService<IMenuService>
    {

        [TestMethod]
        public void TestGet()
        {
            var dd = Service.SelectByUserID(0);
            Assert.IsNotNull(dd);
        }
    }
}
