using System.ComponentModel.Composition;
using System.Windows.Controls;
using Intime.OPC.Modules.GoodsReturn.ViewModel;

namespace Intime.OPC.Modules.GoodsReturn.Views
{
    /// <summary>
    /// Interaction logic for MiniIntimeReturnConsignment.xaml
    /// </summary>
    [Export("MiniIntimeReturnConsignment", typeof(UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class MiniIntimeReturnConsignment : UserControl
    {
        public MiniIntimeReturnConsignment()
        {
            InitializeComponent();
        }

        [Import(typeof(MiniIntimeReturnConsignmentViewModel))]
        public MiniIntimeReturnConsignmentViewModel ViewModel
        {
            set { DataContext = value; }
            get { return DataContext as MiniIntimeReturnConsignmentViewModel; }
        }
    }
}
