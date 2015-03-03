using System.ComponentModel.Composition;
using System.Windows.Controls;
using Intime.OPC.Modules.Authority.ViewModels;
using System.Windows;

namespace Intime.OPC.Modules.Authority.Views
{
    [Export]
    public partial class AuthNavigationItemView : UserControl
    {
        [ImportingConstructor]
        public AuthNavigationItemView(AuthNavaeigationItemViewModel viewModel)
        {
            InitializeComponent();

            ViewMode = viewModel;
        }

        public AuthNavaeigationItemViewModel ViewMode
        {
            get { return (AuthNavaeigationItemViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}