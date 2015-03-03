using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Intime.OPC.Modules.Authority.Views;
using Intime.OPC.Infrastructure;

namespace Intime.OPC.Modules.Authority
{
    [ModuleExport(typeof (AuthorityModule))]
    public class AuthorityModule : IModule
    {
        [Import] public IRegionManager RegionManager;

        public void Initialize()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof (AuthNavigationItemView));
        }
    }
}