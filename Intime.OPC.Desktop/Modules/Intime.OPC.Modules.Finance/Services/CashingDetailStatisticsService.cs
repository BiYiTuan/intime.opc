using System.ComponentModel.Composition;
using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Domain.Dto.Financial;

namespace Intime.OPC.Modules.Finance.Services
{
    [Export(typeof(IService<WebSiteCashierSearchDto>))]
    public class CashingDetailStatisticsService : ServiceBase<WebSiteCashierSearchDto>
    {
    }
}
