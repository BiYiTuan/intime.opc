using System.Collections.Generic;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure.DataService;

namespace Intime.OPC.DataService.Interface
{
    public interface IOrganizationService : IBaseDataService<OPC_OrgInfo>
    {
        IList<OPC_OrgInfo> Search();
    }
}