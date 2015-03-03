using System.ComponentModel.Composition;
using System.Windows.Controls;
using Intime.OPC.Modules.Finance.ViewModels;

namespace Intime.OPC.Modules.Finance.Views
{
    /// <summary>
    /// CashedCommisionStatisticsView.xaml 的交互逻辑
    /// </summary>
    [Export("CashedCommisionStatisticsView", typeof(UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class CashedCommisionStatisticsView : UserControl
    {
        public CashedCommisionStatisticsView()
        {
            InitializeComponent();
        }

        [Import]
        public CashedCommisionStatisticsViewModel ViewModel
        {
            set { DataContext = value; }
            get { return DataContext as CashedCommisionStatisticsViewModel; }
        }
    }
}