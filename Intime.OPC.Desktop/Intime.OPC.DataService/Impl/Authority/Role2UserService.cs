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
    [Export(typeof (IRole2UserService))]
    public class Role2UserService : IRole2UserService
    {
        public ResultMsg SetUserByRole(int roleId, List<int> listUserId)
        {
            try
            {
                bool bFalg = RestClient.Post("role/setUsers", new RoleUserDto {RoleId = roleId, UserIds = listUserId});
                return new ResultMsg {IsSuccess = true, Msg = "保存成功"};
            }
            catch (Exception ex)
            {
                return new ResultMsg {IsSuccess = false, Msg = "API发送失败"};
            }
        }


        public List<OPC_AuthUser> GetUserListByRole(int roleId)
        {
            try
            {
                return
                    RestClient.Get<OPC_AuthUser>("account/GetUsersByRoleID", string.Format("roleId={0}", roleId))
                        .ToList();
            }
            catch (Exception ex)
            {
                return new List<OPC_AuthUser>();
            }
        }
    }
}