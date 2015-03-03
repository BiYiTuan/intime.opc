using System.Collections.Generic;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure.DataService;

namespace Intime.OPC.DataService.Interface
{
    public interface IRole2MenuService
    {
        ResultMsg SetMenuByRole(int roleId, List<int> listMenuId);
        List<OPC_AuthMenu> GetMenuList(int roleId);
    }
}