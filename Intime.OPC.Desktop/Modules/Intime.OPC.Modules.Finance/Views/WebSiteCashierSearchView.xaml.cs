using System.ComponentModel.Composition;
using System.Windows.Controls;
using Intime.OPC.Modules.Finance.ViewModels;

namespace Intime.OPC.Modules.Finance.Views
{
    /// <summary>
    ///     WebSiteCashierSearchView.xaml 的交互逻辑
    /// </summary>
    [Export("WebSiteCashierSearchView", typeof (UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class WebSiteCashierSearchView : UserControl
    {
        public WebSiteCashierSearchView()
        {
            InitializeComponent();
        }

        [Import("WebSiteCashierSearchViewModel")]
        public WebSiteCashierSearchViewModel ViewModel
        {
            set { DataContext = value; }
            get { return DataContext as WebSiteCashierSearchViewModel; }
        }
    }
}