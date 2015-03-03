using System.Collections.Generic;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository
{
    public interface IMenuRepository : IRepository<OPC_AuthMenu>
    {
        IList<OPC_AuthMenu> GetMenusByUserID(int userID);

        IList<OPC_AuthMenu> GetMenusByRoleID(int roleID);

        IList<OPC_AuthMenu> GetMenuList();
    }
}