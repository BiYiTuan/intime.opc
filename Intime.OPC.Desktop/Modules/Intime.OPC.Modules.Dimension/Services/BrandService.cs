using System.ComponentModel.Composition;
using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Modules.Dimension.Common;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Modules.Dimension.Services
{
    [Export(typeof(IService<Brand>))]
    public class BrandService : ServiceBase<Brand>
    {
        
    }
}
