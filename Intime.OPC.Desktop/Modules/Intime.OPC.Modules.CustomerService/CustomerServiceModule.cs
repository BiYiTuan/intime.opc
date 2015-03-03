using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;

namespace Intime.OPC.Modules.CustomerService
{
    [ModuleExport(typeof (CustomerServiceModule))]
    public class CustomerServiceModule : IModule
    {
        [Import]
        public IRegionManager RegionManager { get; set; }

        public void Initialize()
        {
            
        }
    }
}