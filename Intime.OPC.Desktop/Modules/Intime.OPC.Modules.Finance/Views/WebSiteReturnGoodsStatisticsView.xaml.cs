using System.ComponentModel.Composition;
using System.Windows.Controls;
using Intime.OPC.Modules.Finance.ViewModels;

namespace Intime.OPC.Modules.Finance.Views
{
    /// <summary>
    ///     WebSiteReturnGoodsStatisticsView.xaml 的交互逻辑
    /// </summary>
    [Export("WebSiteReturnGoodsStatisticsView", typeof (UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class WebSiteReturnGoodsStatisticsView : UserControl
    {
        public WebSiteReturnGoodsStatisticsView()
        {
            InitializeComponent();
        }

        [Import("WebSiteReturnGoodsStatisticsViewModel")]
        public WebSiteReturnGoodsStatisticsViewModel ViewModel
        {
            set { DataContext = value; }
            get { return DataContext as WebSiteReturnGoodsStatisticsViewModel; }
        }
    }
}