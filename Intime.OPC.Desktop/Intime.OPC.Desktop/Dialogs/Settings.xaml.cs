using MahApps.Metro.Controls;
using Intime.OPC.Desktop.ViewModels;

namespace Intime.OPC.Desktop.Dialogs
{
    public partial class Settings : Flyout
    {
        public Settings()
        {
            InitializeComponent();

            DataContext = new SettingsViewModel();
        }
    }
}
