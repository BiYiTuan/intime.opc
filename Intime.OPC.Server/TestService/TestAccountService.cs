using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain.Models;
using Intime.OPC.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Converters;

namespace TestService
{
    
    [TestClass]
    public class TestAccountService : TestService<IAccountService>
    {

        [TestMethod]
        public void TestGet()
        {
            var dd = Service.Get("admin", "admin");
            Assert.AreEqual(dd.Name,"admin");
        }
        
        public void TestDelete()
        {
            var dd = Service.DeleteById(20);
            Assert.AreNotEqual(dd,true);
        }

        [TestMethod]
        public void TestGetUsersByRoleID()
        {
            var dd = Service.GetUsersByRoleID(6,1,1000);
            Assert.AreEqual(dd, "wxh");
        }

        
        public void TestAddUser()
        {
            OPC_AuthUser u=new OPC_AuthUser();
            u.Name = "test1";
            u.LogonName = "test3";
            u.OrgId = "O";
            u.OrgName = "testOrg";
            u.Password = "123";
            u.CreateDate = DateTime.Now;
            u.CreateUserId = 1;
            u.UpdateDate = DateTime.Now;
            u.UpdateUserId = 1;
            var dd = Service.Add(u);
            Assert.AreEqual(dd, "wxh");
        }
    }
}
