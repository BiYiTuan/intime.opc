using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository
{
    public interface IRoleUserRepository : IRepository<OPC_AuthRoleUser>
    {
        bool UserHasRole(int uId, int roleId);
        void DeleteByUserID(int id);
    }
}