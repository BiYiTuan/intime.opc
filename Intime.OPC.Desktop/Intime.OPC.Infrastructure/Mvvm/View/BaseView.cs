using System.Windows;
using MahApps.Metro.Controls;

namespace Intime.OPC.Infrastructure.Mvvm.View
{
    public class BaseView : MetroWindow, IBaseView
    {
        public void Cancel()
        {
            DialogResult = false;
            base.Close();
        }


        public void CloseView()
        {
            string error = ValidModel();
            if (!string.IsNullOrWhiteSpace(error))
            {
                MessageBox.Show(error);
                return;
            }
            DialogResult = true;
            base.Close();
        }

        protected virtual string ValidModel()
        {
            return "";
        }
    }
}