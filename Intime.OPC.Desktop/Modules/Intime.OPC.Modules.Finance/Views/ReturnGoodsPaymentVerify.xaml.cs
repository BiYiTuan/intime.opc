using System.ComponentModel.Composition;
using System.Windows.Controls;
using Intime.OPC.Modules.Finance.ViewModels;

namespace Intime.OPC.Modules.Finance.Views
{
    /// <summary>
    ///     ReturnPackageManageView.xaml 的交互逻辑
    /// </summary>
    [Export("ReturnGoodsPaymentVerify", typeof (UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ReturnGoodsPaymentVerify : UserControl
    {
        public ReturnGoodsPaymentVerify()
        {
            InitializeComponent();
        }

        [Import("ReturnPackageManageViewModel")]
        public ReturnGoodsPaymentVerifyViewModel ViewModel
        {
            set { DataContext = value; }
            get { return DataContext as ReturnGoodsPaymentVerifyViewModel; }
        }
    }
}