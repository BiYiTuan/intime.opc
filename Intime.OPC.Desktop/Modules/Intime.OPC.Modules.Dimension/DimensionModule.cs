using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace Intime.OPC.Modules.Dimension
{
    [ModuleExport(typeof (DimensionModule))]
    public class DimensionModule : IModule
    {
        [Import] 
        public IRegionManager RegionManager { get; set; }

        public void Initialize()
        {
            
        }
    }
}