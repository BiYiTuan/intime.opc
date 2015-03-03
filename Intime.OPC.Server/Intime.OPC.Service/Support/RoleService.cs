using System;
using System.Collections.Generic;
using System.Linq;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository;

namespace Intime.OPC.Service.Support
{
    public class RoleService : BaseService<OPC_AuthRole>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleMenuRepository _roleMenuRepository;
        private IRoleUserRepository _roleUserRepository;

        public RoleService(IRoleRepository roleRepository, IRoleMenuRepository roleMenuRepository, IRoleUserRepository roleUserRepository)
            : base(roleRepository)
        {
            _roleRepository = roleRepository;
            _roleMenuRepository = roleMenuRepository;
            _roleUserRepository = roleUserRepository;
        }

        #region IRoleService Members


        public PageResult<OPC_AuthRole> Select(int pageIndex, int pageSize)
        {
            return _repository.GetAll(pageIndex, pageSize);
        }

        public bool IsStop(int roleId, bool bValid)
        {
            return _roleRepository.SetEnable(roleId, bValid);
        }

        public bool SetMenus(int roleId, int userID, IEnumerable<int> menuids)
        {
            var bl = _roleMenuRepository.DeleteByRoleMenu(roleId);
            if (bl)
            {
                return _roleMenuRepository.AddMenus(roleId, userID, menuids);
            }
            return false;
        }

        public PageResult<OPC_AuthRole> GetByUserID(int userID, int pageIndex, int pageSize = 20)
        {
            return _roleRepository.GetByUserID(userID, pageIndex, pageSize);
        }

        public bool SetUsers(RoleUserDto roleUserDto, int userId)
        {
            foreach (var uId in roleUserDto.UserIds)
            {
                bool bl = _roleUserRepository.UserHasRole(uId, roleUserDto.RoleId);
                if (!bl)
                {
                    var userRole = new OPC_AuthRoleUser();
                    userRole.CreateDate = DateTime.Now;
                    userRole.CreateUserId = userId;
                    userRole.UpdateDate = DateTime.Now;
                    userRole.UpdateUserId = userId;
                    userRole.OPC_AuthUserId = uId;
                    userRole.OPC_AuthRoleId = roleUserDto.RoleId;

                    _roleUserRepository.Create(userRole);
                }

            }
            return true;
        }

        public IEnumerable<OPC_AuthRole> GetRolesByUserId(int userId)
        {
            return _roleRepository.GetRolesByUserId(userId) ?? new List<OPC_AuthRole>();
        }

        #endregion
    }
}