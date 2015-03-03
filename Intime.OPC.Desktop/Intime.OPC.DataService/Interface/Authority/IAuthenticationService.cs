using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure.DataService;

namespace Intime.OPC.DataService.Interface
{
    public interface IAuthenticationService : IBaseDataService<OPC_AuthUser>
    {
        string Login(string userName, string password);
        bool SetIsStop(OPC_AuthUser user);
        ResultMsg ResetPassword(OPC_AuthUser user);

        ResultMsg UpdatePassword(OPC_AuthUser user,string newPwd);
    }
}