using System.ComponentModel.Composition;
using System.Windows.Controls;
using Intime.OPC.Modules.Authority.ViewModels;

namespace Intime.OPC.Modules.Authority.Views
{
    /// <summary>
    ///     Role2UserListWindow.xaml 的交互逻辑
    /// </summary>
    [Export("Role2MenuWindow", typeof (UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class Role2MenuWindow : UserControl
    {
        public Role2MenuWindow()
        {
            InitializeComponent();
        }

        [Import("Role2MenuViewModel", typeof (Role2MenuWindowViewModel))]
        public Role2MenuWindowViewModel ViewModel
        {
            get { return DataContext as Role2MenuWindowViewModel; }
            set { DataContext = value; }
        }
    }
}