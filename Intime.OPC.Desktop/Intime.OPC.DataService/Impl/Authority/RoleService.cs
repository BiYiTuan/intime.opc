using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Intime.OPC.DataService.Common;
using Intime.OPC.DataService.Interface;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;
using Intime.OPC.Infrastructure.DataService;

namespace Intime.OPC.DataService.Impl.Auth
{
    [Export(typeof (IRoleService))]
    public class RoleService : IRoleService
    {
        private string url = "Role/AddRole";

        public ResultMsg Add(OPC_AuthRole model)
        {
            bool result = RestClient.Post("Role/AddRole", model);
            if (result)
            {
                return ResultMsg.Success();
            }
            return ResultMsg.Failure("添加失败！");
        }

        public ResultMsg Edit(OPC_AuthRole model)
        {
            bool result = RestClient.Put("Role/UpdateRole", model);
            if (result)
            {
                return ResultMsg.Success();
            }
            return ResultMsg.Failure("更新失败！");
        }

        public ResultMsg Delete(OPC_AuthRole model)
        {
            bool result = RestClient.Put("Role/DeleteRole", model.Id);
            if (result)
            {
                return ResultMsg.Success();
            }
            return ResultMsg.Failure("删除失败！");
        }


        public PageResult<OPC_AuthRole> Search(IDictionary<string, object> iDicFilter)
        {
            IList<OPC_AuthRole> result = RestClient.Get<OPC_AuthRole>("Role/SelectRole");
            return new PageResult<OPC_AuthRole>(result, result.Count);
        }

        public bool SetIsEnable(OPC_AuthRole role)
        {
            try
            {
                bool bFalg = RestClient.Put("role/Enable", role);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}