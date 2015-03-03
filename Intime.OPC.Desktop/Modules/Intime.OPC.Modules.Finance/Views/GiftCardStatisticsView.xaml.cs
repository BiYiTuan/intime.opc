using System.ComponentModel.Composition;
using System.Windows.Controls;
using Intime.OPC.Modules.Finance.ViewModels;

namespace Intime.OPC.Modules.Finance.Views
{
    /// <summary>
    /// CashedCommisionStatisticsView.xaml 的交互逻辑
    /// </summary>
    [Export("GiftCardStatisticsView", typeof(UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class GiftCardStatisticsView : UserControl
    {
        public GiftCardStatisticsView()
        {
            InitializeComponent();
        }

        [Import]
        public GiftCardStatisticsViewModel ViewModel
        {
            set { DataContext = value; }
            get { return DataContext as GiftCardStatisticsViewModel; }
        }
    }
}