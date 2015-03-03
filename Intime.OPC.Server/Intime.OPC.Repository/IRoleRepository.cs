using System.Collections.Generic;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository
{
    public interface IRoleRepository : IRepository<OPC_AuthRole>
    {
        PageResult<OPC_AuthRole> All(int pageIndex, int pageSize = 20);
        PageResult<OPC_AuthRole> GetByUserID(int userID, int pageIndex, int pageSize = 20);
        bool SetEnable(int roleID, bool enable);

        /// <summary>
        /// 根据用户获取角色列表
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>角色列表</returns>
        IEnumerable<OPC_AuthRole> GetRolesByUserId(int userId);
    }
}