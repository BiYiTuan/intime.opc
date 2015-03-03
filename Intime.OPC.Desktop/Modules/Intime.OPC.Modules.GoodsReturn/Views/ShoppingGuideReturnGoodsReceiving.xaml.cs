using System.ComponentModel.Composition;
using System.Windows.Controls;
using OPCApp.ReturnGoodsManage.ViewModel;

namespace OPCApp.ReturnGoodsManage.Views
{
    /// <summary>
    /// ShopperReturnGoodsSearchView.xaml 的交互逻辑
    /// </summary>
    [Export("ShopperReturnGoodsSearchView", typeof(UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ShopperReturnGoodsSearchView : UserControl
    {
        public ShopperReturnGoodsSearchView()
        {
            InitializeComponent();
        }
        [Import(typeof(ShopperReturnGoodsSearchViewModel))]
        public ShopperReturnGoodsSearchViewModel ViewModel
        {
            set { DataContext = value; }
            get { return DataContext as ShopperReturnGoodsSearchViewModel; }
        }
    }
}
