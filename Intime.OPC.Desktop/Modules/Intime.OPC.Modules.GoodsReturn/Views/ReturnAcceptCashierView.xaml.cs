using System.ComponentModel.Composition;
using System.Windows.Controls;
using Intime.OPC.Modules.GoodsReturn.ViewModel;

namespace Intime.OPC.Modules.GoodsReturn.View
{
    /// <summary>
    ///     ReturnAcceptCashierView.xaml 的交互逻辑
    /// </summary>
    [Export("ReturnAcceptCashierView", typeof (UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ReturnAcceptCashierView : UserControl
    {
        public ReturnAcceptCashierView()
        {
            InitializeComponent();
        }

        [Import(typeof (ReturnAcceptCashierViewModel))]
        public ReturnAcceptCashierViewModel ViewModel
        {
            set { DataContext = value; }
            get { return DataContext as ReturnAcceptCashierViewModel; }
        }
    }
}