﻿using System.ComponentModel.Composition;
using Intime.OPC.Domain.Dto.Financial;
using Intime.OPC.Infrastructure.Service;

namespace Intime.OPC.Modules.Finance.Services
{
    [Export(typeof(IService<GiftCardStatisticsDto>))]
    public class GiftCardStatisticsService : ServiceBase<GiftCardStatisticsDto>
    {
    }
}
