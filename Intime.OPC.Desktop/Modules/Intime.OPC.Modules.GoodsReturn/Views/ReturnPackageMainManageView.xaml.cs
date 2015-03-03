using System.ComponentModel.Composition;
using System.Windows.Controls;
using Intime.OPC.Modules.GoodsReturn.ViewModel;

namespace Intime.OPC.Modules.GoodsReturn.View
{
    /// <summary>
    ///     ReturnAcceptCashierView.xaml 的交互逻辑
    /// </summary>
    [Export("ReturnPackageMainManageView", typeof (UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ReturnPackageMainManageView : UserControl
    {
        public ReturnPackageMainManageView()
        {
            InitializeComponent();
        }

        [Import("ReturnPackageMainViewModel")]
        public ReturnPackageMainViewModel ViewModel
        {
            set { DataContext = value; }
            get { return DataContext as ReturnPackageMainViewModel; }
        }
    }
}