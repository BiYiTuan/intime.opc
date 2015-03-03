using System.ComponentModel.Composition;
using System.Windows.Controls;
using Intime.OPC.Modules.GoodsReturn.ViewModel;

namespace Intime.OPC.Modules.GoodsReturn.Views
{
    /// <summary>
    ///     ReturnGoodsInStorageView.xaml 的交互逻辑
    /// </summary>
    [Export("ReturnGoodsInStorageView", typeof (UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ReturnGoodsInStorageView : UserControl
    {
        public ReturnGoodsInStorageView()
        {
            InitializeComponent();
        }

        [Import(typeof (ReturnGoodsInStorageViewModel))]
        public ReturnGoodsInStorageViewModel ViewModel
        {
            set { DataContext = value; }
            get { return DataContext as ReturnGoodsInStorageViewModel; }
        }
    }
}