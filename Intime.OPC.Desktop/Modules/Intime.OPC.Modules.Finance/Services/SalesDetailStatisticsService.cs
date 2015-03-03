using System.ComponentModel.Composition;
using Intime.OPC.Infrastructure.Service;
using OPCAPP.Domain.Dto.Financial;

namespace Intime.OPC.Modules.Finance.Services
{
    [Export(typeof(IService<WebSiteSalesStatisticsDto>))]
    public class SalesDetailStatisticsService : ServiceBase<WebSiteSalesStatisticsDto>
    {
    }
}
