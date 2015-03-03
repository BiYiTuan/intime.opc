using System.Collections.Generic;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository
{
    public interface IRoleMenuRepository : IRepository<OPC_AuthRoleMenu>
    {
        /// <summary>
        /// 根据角色删除菜单
        /// </summary>
        /// <param name="roleID">The role identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool DeleteByRoleMenu(int roleID);

        /// <summary>
        /// 添加用户的菜单
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="menuIds">The menu ids.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool AddMenus(int role, int userId, IEnumerable<int> menuIds);
    }
}