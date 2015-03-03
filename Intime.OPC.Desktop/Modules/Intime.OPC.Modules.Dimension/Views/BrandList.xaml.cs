using System.ComponentModel.Composition;
using System.Windows.Controls;
using Intime.OPC.Modules.Dimension.ViewModels;

namespace Intime.OPC.Modules.Dimension.Views
{
    /// <summary>
    /// Interaction logic for BrandList.xaml
    /// </summary>
    [Export("BrandList", typeof(UserControl))]
    public partial class BrandList : UserControl
    {
        public BrandList()
        {
            InitializeComponent();
        }

        [Import]
        public BrandListViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}
