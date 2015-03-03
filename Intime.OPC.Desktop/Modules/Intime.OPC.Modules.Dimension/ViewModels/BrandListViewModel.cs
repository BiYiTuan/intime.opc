using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Intime.OPC.Infrastructure.Mvvm;
using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Modules.Dimension.Common;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Modules.Dimension.ViewModels
{
    [Export(typeof(BrandListViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BrandListViewModel : DimensionListViewModel<Brand,BrandViewModel,IService<Brand>>
    {
        public BrandListViewModel()
        {
            EnableCommand = new AsyncDelegateCommand(() => OnEnable(true), CanExecute);
            DisableCommand = new AsyncDelegateCommand(() => OnEnable(false), CanExecute);
        }

        #region Commands

        public ICommand DisableCommand { get; private set; }

        public ICommand EnableCommand { get; private set; }

        #endregion

        #region Command handler

        private void OnEnable(bool enabled)
        {
            Models.ForEach(brand => 
            {
                if (brand.IsSelected && brand.Enabled != enabled)
                {
                    brand.Enabled = enabled;
                    Service.Update(brand);
                }
            });
        }
        #endregion
    }
}
