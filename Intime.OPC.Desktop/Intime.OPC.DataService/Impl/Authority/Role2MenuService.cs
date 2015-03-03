using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Intime.OPC.DataService.Common;
using Intime.OPC.DataService.Interface;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure.DataService;

namespace Intime.OPC.DataService.Impl.Auth
{
    [Export(typeof (IRole2MenuService))]
    public class Role2MenuService : IRole2MenuService
    {
        public ResultMsg SetMenuByRole(int roleId, List<int> listMenuId)
        {
            try
            {
                bool bFalg = RestClient.Post("role/setmenus", new RoleMenuDto {RoleID = roleId, MenuList = listMenuId});
                return new ResultMsg {IsSuccess = true, Msg = "保存成功"};
            }
            catch (Exception ex)
            {
                return new ResultMsg {IsSuccess = false, Msg = "API发送失败"};
            }
        }


        public List<OPC_AuthMenu> GetMenuList(int roleId)
        {
            try
            {
                return
                    RestClient.Get<OPC_AuthMenu>("menu/LoadMenuByRoleID", string.Format("roleId={0}", roleId)).ToList();
            }
            catch (Exception ex)
            {
                return new List<OPC_AuthMenu>();
            }
        }
    }
}