using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Mvvm;
using Intime.OPC.Modules.GoodsReturn.ViewModels;

namespace Intime.OPC.Modules.GoodsReturn.ViewModel
{
    [Export("ReturnPackageMainViewModel", typeof (ReturnPackageMainViewModel))]
    public class ReturnPackageMainViewModel : BindableBase
    {
        private ReturnPackageVerifyViewViewModel _customerPackageVerifyViewModel;
        private ReturnPackageManageViewModel _returnPackageManageViewModel;
        private ReturnPackagePrintExpressageConnectViewModel _returnPackagePrintConnectViewModel;
        private ReturnPackagePrintExpressViewModel _returnPackagePrintExpressViewModel;

        [Import]
        public ReturnPackageManageViewModel ReturnPackageManageViewModel
        {
            get { return _returnPackageManageViewModel; }
            set { SetProperty(ref _returnPackageManageViewModel, value); }
        }

        [Import]
        public ReturnPackageVerifyViewViewModel ReturnPackageVerifyViewViewModel
        {
            get { return _customerPackageVerifyViewModel; }
            set { SetProperty(ref _customerPackageVerifyViewModel, value); }
        }

        [Import]
        public ReturnPackagePrintExpressViewModel ReturnPackagePrintExpressViewModel
        {
            get { return _returnPackagePrintExpressViewModel; }
            set { SetProperty(ref _returnPackagePrintExpressViewModel, value); }
        }

        [Import]
        public ReturnPackagePrintExpressageConnectViewModel ReturnPackagePrintConnectViewModel
        {
            get { return _returnPackagePrintConnectViewModel; }
            set { SetProperty(ref _returnPackagePrintConnectViewModel, value); }
        }
    }
}