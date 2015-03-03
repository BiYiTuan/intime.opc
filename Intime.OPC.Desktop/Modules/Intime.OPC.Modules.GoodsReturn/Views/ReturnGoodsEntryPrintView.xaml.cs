using System.ComponentModel.Composition;
using System.Windows.Controls;
using Intime.OPC.Modules.GoodsReturn.ViewModel;

namespace Intime.OPC.Modules.GoodsReturn.Views
{
    /// <summary>
    ///     ReturnGoodsEntryPrintView.xaml 的交互逻辑
    /// </summary>
    [Export("ReturnGoodsEntryPrintView", typeof (UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ReturnGoodsEntryPrintView : UserControl
    {
        public ReturnGoodsEntryPrintView()
        {
            InitializeComponent();
        }

        [Import(typeof (ReturnGoodsEntryPrintViewModel))]
        public ReturnGoodsEntryPrintViewModel ViewModel
        {
            set { DataContext = value; }
            get { return DataContext as ReturnGoodsEntryPrintViewModel; }
        }
    }
}