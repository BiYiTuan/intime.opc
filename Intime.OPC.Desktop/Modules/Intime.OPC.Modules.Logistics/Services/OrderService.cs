using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure.REST;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Modules.Logistics.Services
{
    [Export(typeof(IService<Order>))]
    public class OrderService : ServiceBase<Order>
    {

    }
}
