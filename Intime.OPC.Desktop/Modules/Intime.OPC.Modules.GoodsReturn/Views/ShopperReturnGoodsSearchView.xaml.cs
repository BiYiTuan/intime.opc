using System.ComponentModel.Composition;
using System.Windows.Controls;
using Intime.OPC.Modules.GoodsReturn.ViewModel;

namespace Intime.OPC.Modules.GoodsReturn.Views
{
    /// <summary>
    ///     ShopperReturnGoodsSearchView.xaml 的交互逻辑
    /// </summary>
    [Export("ShopperReturnGoodsSearchView", typeof (UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ShopperReturnGoodsSearchView : UserControl
    {
        public ShopperReturnGoodsSearchView()
        {
            InitializeComponent();
        }

        [Import(typeof (ShopperReturnGoodsSearchViewModel))]
        public ShopperReturnGoodsSearchViewModel ViewModel
        {
            set { DataContext = value; }
            get { return DataContext as ShopperReturnGoodsSearchViewModel; }
        }

    }
}