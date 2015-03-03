using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Intime.OPC.Infrastructure.Print;

namespace Intime.OPC.Modules.GoodsReturn
{
    [ModuleExport(typeof(GoodsReturnModule))]
    public class GoodsReturnModule : IModule
    {
        [Import] 
        public IRegionManager RegionManager { get; set; }

        public void Initialize()
        {

        }
    }
}