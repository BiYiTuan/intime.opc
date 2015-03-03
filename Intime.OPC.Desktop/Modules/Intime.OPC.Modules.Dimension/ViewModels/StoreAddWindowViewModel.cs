using System.ComponentModel.Composition;
using Intime.OPC.DataService.Interface;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;
using Intime.OPC.Infrastructure.DataService;
using Intime.OPC.Infrastructure.Mvvm;

namespace Intime.OPC.Modules.Dimension.ViewModels
{
    [Export("StoreViewModel", typeof (IViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StoreAddWindowViewModel : BaseViewModel<Store>
    {
        public StoreAddWindowViewModel()
            : base("StoreView")
        {
            Model = new Store();
        }

        protected override IBaseDataService<Store> GetDataService()
        {
            return AppEx.Container.GetInstance<IStoreDataService>();
        }
    }
}