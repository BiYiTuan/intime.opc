using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Mvvm;

namespace Intime.OPC.Modules.CustomerService.ViewModels
{
    [Export("CustomerReturnGoodsMainViewModel", typeof (CustomerReturnGoodsMainViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CustomerReturnGoodsMainViewModel : BindableBase
    {
        private CustomerReturnSearchRmaViewModel _customerReturnSearch;
        private CustomerReturnSearchFinancialViewModel _customerReturnSearchFinancialView;

        private CustomerReturnSearchTransViewModel _customerReturnSearchTransViewModel;

        [Import]
        public CustomerReturnSearchFinancialViewModel CustomerReturnSearchFinancialViewModel
        {
            get { return _customerReturnSearchFinancialView; }
            set { SetProperty(ref _customerReturnSearchFinancialView, value); }
        }

        [Import]
        public CustomerReturnSearchTransViewModel CustomerReturnSearchTransViewModel

        {
            get { return _customerReturnSearchTransViewModel; }
            set { SetProperty(ref _customerReturnSearchTransViewModel, value); }
        }


        [Import]
        public CustomerReturnSearchRmaViewModel CustomerReturnSearchRmaViewModel
        {
            get { return _customerReturnSearch; }
            set { SetProperty(ref _customerReturnSearch, value); }
        }
    }
}