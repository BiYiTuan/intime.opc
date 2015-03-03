using System;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Repository;
using Intime.OPC.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestService
{
    [TestClass]
    public class TestRoleService : TestService<IRoleService>
    {
        [TestMethod]
        public void TestSetUsers()
        {
            RoleUserDto dto=new RoleUserDto();
            dto.RoleId = 3;
            dto.UserIds.Add(0);
            dto.UserIds.Add(2);
            dto.UserIds.Add(5);
            var count = Service.SetUsers(dto, 1);
            Assert.IsNotNull(count);
        }
    }
}
