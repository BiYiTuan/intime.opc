using System.Collections.Generic;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure.DataService;

namespace Intime.OPC.DataService.Interface
{
    public interface IRole2UserService
    {
        ResultMsg SetUserByRole(int roleId, List<int> listUserId);
        List<OPC_AuthUser> GetUserListByRole(int roleId);
    }
}