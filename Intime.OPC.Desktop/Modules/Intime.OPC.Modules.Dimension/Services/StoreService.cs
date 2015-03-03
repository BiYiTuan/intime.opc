using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Modules.Dimension.Services
{
    [Export(typeof(IService<Store>))]
    public class StoreService : ServiceBase<Store>
    {

    }
}
