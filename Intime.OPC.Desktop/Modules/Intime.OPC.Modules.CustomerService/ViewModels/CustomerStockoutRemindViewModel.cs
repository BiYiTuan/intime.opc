using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;

namespace Intime.OPC.Modules.CustomerService.ViewModels
{
    [Export(typeof(CustomerStockoutRemindViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public  class CustomerStockoutRemindViewModel:BindableBase
    {

        private CustomerStockoutRemindNotReplenishViewModel _customerStockoutRemindNotReplenishViewModel;

        [Import(typeof(CustomerStockoutRemindNotReplenishViewModel))]
        public CustomerStockoutRemindNotReplenishViewModel CustomerStockoutRemindNotReplenishViewModel
        {
            get { return _customerStockoutRemindNotReplenishViewModel; }
            set { SetProperty(ref _customerStockoutRemindNotReplenishViewModel, value); }
        }
        private CustomerStockoutRemindCommonViewModel _customerStockoutRemindCommonViewModel;

        [Import(typeof(CustomerStockoutRemindCommonViewModel))]
        public CustomerStockoutRemindCommonViewModel CustomerStockoutRemindCommonViewModel
        {
            get { return _customerStockoutRemindCommonViewModel; }
            set { SetProperty(ref _customerStockoutRemindCommonViewModel, value); }
        }
    }
}
