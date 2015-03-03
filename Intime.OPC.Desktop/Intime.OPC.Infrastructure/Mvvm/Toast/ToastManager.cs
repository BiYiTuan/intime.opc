using System;

namespace Intime.OPC.Infrastructure.Mvvm.Toast
{
    public class ToastManager
    {
        public static void ShowToast(string message, Action callback = null)
        {
            Action showToast = () => 
            {
                
                var dialog = new NotificationDialog { Message = message, Action = callback };
                dialog.ShowToast();
            };

            var manager = new SingletonWindowManager<NotificationDialog>(showToast);
            manager.DisplayWindow();
        }
    }
}
