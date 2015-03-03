using System.Collections.Generic;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Service
{
    public interface IAccountService : IService, ICanAdd<OPC_AuthUser>, ICanDelete, ICanUpdate<OPC_AuthUser>
    {
        AuthUserDto Get(string userName, string password);

        PageResult<AuthUserDto> Select(string orgid, string name, int pageIndex, int pageSize = 20);
        PageResult<AuthUserDto> SelectByLogName(string orgid, string loginName, int pageIndex, int pageSize = 20);
        bool IsStop(int userId, bool bValid);

        PageResult<AuthUserDto> GetUsersByRoleID(int roleId, int pageIndex, int pageSize = 20);

        UserDto GetByUserID(int userID);



        void ResetPassword(int userId);
        void ChangePassword(int userid, string oldpassword, string newpassword);



        /// <summary>
        /// 获取 OPC 后台用户
        /// </summary>
        /// <param name="pagerRequest">分页参数</param>
        /// <param name="roleId">指定权限</param>
        /// <param name="authdatastartsWith">AUTHDATA  前拽</param>
        /// <param name="incloudeSystem">是否包含管理员帐号</param>
        /// <returns></returns>
        PageResult<AuthUserDto> GetPagedList(PagerRequest pagerRequest, int? roleId, List<string> authdatastartsWith,
                                             bool? incloudeSystem, string name = null, string logonName = null);

    }
}