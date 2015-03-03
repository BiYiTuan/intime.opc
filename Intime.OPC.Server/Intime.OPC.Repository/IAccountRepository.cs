using System.Collections.Generic;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository
{
    public interface IAccountRepository : IRepository<OPC_AuthUser>
    {
        OPC_AuthUser Get(string userName, string password);
        PageResult<OPC_AuthUser> All(int pageIndex, int pageSize = 20);
        bool SetEnable(int userId, bool enable);

        PageResult<OPC_AuthUser> GetByRoleId(int roleId, int pageIndex, int pageSize);
        PageResult<OPC_AuthUser> GetByLoginName(string orgID, string loginName, int pageIndex, int pageSize);

        PageResult<OPC_AuthUser> GetByOrgId(string orgID, string name, int pageIndex, int pageSize);

        /// <summary>
        /// 获取 OPC 后台用户
        /// </summary>
        /// <param name="pagerRequest">分页参数</param>
        /// <param name="roleId">指定权限</param>
        /// <param name="authdatastartsWith">AUTHDATA  前拽</param>
        /// <param name="incloudeSystem">是否包含管理员帐号</param>
        /// <returns></returns>
        PagerInfo<OPC_AuthUser> GetPagedList(PagerRequest pagerRequest, int? roleId, List<string> authdatastartsWith,
                                             bool? incloudeSystem, string name = null, string logonName = null);
    }
}