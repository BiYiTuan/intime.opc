using System.ComponentModel.Composition;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure.Service;

namespace Intime.OPC.Modules.Dimension.Services
{
    [Export(typeof(IStoreApplicationService))]
    public class StoreApplicationService : ServiceBase<ApplicationInfo>, IStoreApplicationService
    {
        public void Approve(ApplicationInfo applicationInfo)
        {
            string uri = string.Format("{0}/{1}/approved", UriName, applicationInfo.Id);
            Update(uri, new { Approved = 1});
        }
    }
}
