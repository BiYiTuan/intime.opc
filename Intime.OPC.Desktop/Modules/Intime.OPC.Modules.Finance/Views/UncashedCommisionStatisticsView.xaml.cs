using System.ComponentModel.Composition;
using System.Windows.Controls;
using Intime.OPC.Modules.Finance.ViewModels;

namespace Intime.OPC.Modules.Finance.Views
{
    /// <summary>
    /// UncashedCommisionStatisticsView.xaml 的交互逻辑
    /// </summary>
    [Export("UncashedCommisionStatisticsView", typeof(UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class UncashedCommisionStatisticsView : UserControl
    {
        public UncashedCommisionStatisticsView()
        {
            InitializeComponent();
        }

        [Import]
        public UncashedCommisionStatisticsViewModel ViewModel
        {
            set { DataContext = value; }
            get { return DataContext as UncashedCommisionStatisticsViewModel; }
        }
    }
}