using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;

namespace Intime.OPC.Modules.Dimension.ViewModels
{
    public class NavigationItemViewModel : BindableBase
    {
        public DelegateCommand UserListCommand { get; set; }
        public DelegateCommand RoleListCommand { get; set; }
    }
}