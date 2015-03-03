using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace Intime.OPC.Modules.GoodsReturn
{
    [ModuleExport(typeof(FinanceModule))]
    public class FinanceModule : IModule
    {
        [Import] 
        public IRegionManager RegionManager { get; set; }

        public void Initialize()
        {
            
        }
    }
}