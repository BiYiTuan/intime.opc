using System.ComponentModel.Composition;
using System.Windows.Controls;
using Intime.OPC.Modules.Finance.ViewModels;

namespace Intime.OPC.Modules.Finance.Views
{
    /// <summary>
    ///     ReturnPackageManageView.xaml 的交互逻辑
    /// </summary>
    [Export("ReturnGoodsCompensateVerify", typeof (UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ReturnGoodsCompensateVerify : UserControl
    {
        public ReturnGoodsCompensateVerify()
        {
            InitializeComponent();
        }

        [Import("ReturnGoodsCompensateVerifyViewModel")]
        public ReturnGoodsCompensateVerifyViewModel ViewModel
        {
            set { DataContext = value; }
            get { return DataContext as ReturnGoodsCompensateVerifyViewModel; }
        }
    }
}