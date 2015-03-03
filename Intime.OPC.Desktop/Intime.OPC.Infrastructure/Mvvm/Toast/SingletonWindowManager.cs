using System;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace Intime.OPC.Infrastructure.Mvvm.Toast
{
    public class SingletonWindowManager<TWindow> where TWindow : Window
    {
        private readonly object _syncObject = new object();
        private readonly Action _displayCallback;
        
        public SingletonWindowManager(Action displayCallback)
        {
            if (displayCallback == null) throw new ArgumentNullException("displayCallback");

            _displayCallback = displayCallback;
        }

        /// <summary>
        /// Close the existing windows with the given type, and display it by invoking the callback.
        /// </summary>
        public void DisplayWindow()
        {
            lock (_syncObject)
            {
                if (Application.Current == null || Application.Current.Dispatcher == null)
                {
                    throw new InvalidOperationException("Unable to find a dispatcher.");
                }

                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                {
                    var activeWindows = Application.Current.Windows;
                    var targetWindows = activeWindows.OfType<TWindow>();
                    //Close all the windows with the same type before displaying
                    var enumerable = targetWindows as TWindow[] ?? targetWindows.ToArray();
                    if (enumerable.Any())
                    {
                        foreach (var window in enumerable.Where(window => window.IsVisible))
                        {
                            window.Close();
                        }
                    }
                    
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        if (_displayCallback != null) _displayCallback();
                    }));
                }));
            }
        }
    }
}
