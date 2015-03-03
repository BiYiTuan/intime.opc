using System;
using System.Collections.Generic;
using System.Linq;

using Intime.OPC.Domain.Models;
using Intime.OPC.Repository;
using Intime.OPC.Service;

namespace Intime.OPC.WebApi.Core.MessageHandlers.AccessToken.Support
{
    public class DefaultUserProfileProvider : IUserProfileProvider
    {
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;

        public DefaultUserProfileProvider(IAccountService accountService, IRoleService roleService)
        {
            _accountService = accountService;
            _roleService = roleService;
        }

        public UserProfile Get(int userId)
        {
            var currentUser = _accountService.GetByUserID(userId);

            if (currentUser == null)
            {
                throw new NullReferenceException(string.Format("当前用户信息不存在,userId:{0}", userId));
            }

            var roles = _roleService.GetRolesByUserId(userId) ?? new List<OPC_AuthRole>();

            // 目前不需要缓存
            return new UserProfile
             {
                 Id = userId,
                 Name = currentUser.Name,
                 IsSystem = currentUser.IsSystem,
                 SectionIds = currentUser.SectionIds ?? new List<int>(0),
                 StoreIds = currentUser.StoreIds ?? new List<int>(0),
                 Roles = roles.ToList().ConvertAll(p => p.Name),
                 OrgIds = currentUser.OrgIds ?? new List<string>(0)
             };
        }
    }
}
