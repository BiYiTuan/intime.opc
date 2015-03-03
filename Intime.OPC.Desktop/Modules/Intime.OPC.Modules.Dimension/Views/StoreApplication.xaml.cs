using System.ComponentModel.Composition;
using System.Windows.Controls;
using Intime.OPC.Modules.Dimension.ViewModels;

namespace Intime.OPC.Modules.Dimension.Views
{
    /// <summary>
    /// Interaction logic for StoreApplication.xaml
    /// </summary>
    [Export("StoreApplication", typeof(UserControl))]
    public partial class StoreApplication : UserControl
    {
        public StoreApplication()
        {
            InitializeComponent();
        }

        [Import]
        public StoreApplicationViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}
