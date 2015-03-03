using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Intime.OPC.Infrastructure.Mvvm.Utility
{
    public class MvvmUtility
    {
        public static void OnException(Exception exception)
        {
            ShowMessageAsync(exception.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void PerformAction(Action action)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                OnException(exception);
            }
        }

        public static void PerformActionOnUIThread(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }

        public static TResult PerformActionOnUIThread<TResult>(Func<TResult> callback)
        {
            return Application.Current.Dispatcher.Invoke(callback);
        }

        public static async void WarnIfEmpty<T>(IEnumerable<T> enumerable, string entityName)
        {
            if (enumerable == null || !enumerable.Any())
            {
                await ShowMessageAsync(string.Format("没有符合条件的{0}", entityName), "提示");
            }
        }

        public static Task<ProgressDialogController> ShowProgressAsync(string message)
        {
            var mainWindow = Application.Current.MainWindow as MetroWindow;
            if (mainWindow == null) return null;

            var dialogSetting = new MetroDialogSettings 
            {
                AffirmativeButtonText = "确定"
            };

            return mainWindow.ShowProgressAsync("请稍候...", message, settings: dialogSetting);
        }

        public static Task<string> ShowInputDialogAsync(string title, string message, string defaultText = null)
        {
            Func<Task<string>> callback = () =>
            {
                var mainWindow = Application.Current.MainWindow as MetroWindow;
                if (mainWindow == null) return null;

                var dialogSetting = new MetroDialogSettings
                {
                    AffirmativeButtonText = "确定",
                    NegativeButtonText = "取消",
                    DefaultText = defaultText,
                    FirstAuxiliaryButtonText = "FirstAuxiliaryButtonText",
                    SecondAuxiliaryButtonText = "FirstAuxiliaryButtonText"
                };

                return mainWindow.ShowInputAsync(title, message, dialogSetting);
            };

            return PerformActionOnUIThread(callback);

        }

        public static Task<MessageDialogResult> ShowMessageAsync(string message, string caption = "提示", MessageBoxButton buttons = MessageBoxButton.OK, MessageBoxImage image = MessageBoxImage.Warning,MetroDialogSettings dialogSetting = null, MessageDialogStyle dialogStyle = MessageDialogStyle.Affirmative)
        {
            Func<Task<MessageDialogResult>> callback = () =>
            {
                var mainWindow = Application.Current.MainWindow as MetroWindow;

                if (mainWindow == null)
                {
                    MessageBox.Show(message, caption, buttons, image);
                    return new Task<MessageDialogResult>(() => MessageDialogResult.Affirmative);
                }
                else
                {
                    if (!mainWindow.IsOverlayVisible())
                    {
                        mainWindow.ShowOverlayAsync();
                    }
                    var defaultDialogSetting = new MetroDialogSettings
                    {
                        AffirmativeButtonText = "确定"
                    };

                    return mainWindow.ShowMessageAsync(caption, message, dialogStyle, dialogSetting ?? defaultDialogSetting);
                }
            };

            return PerformActionOnUIThread(callback);
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static T FindParent<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                var parent = VisualTreeHelper.GetParent(depObj);
                if (parent != null)
                {
                    if (parent is T)
                    {
                        return (T)parent;
                    }
                    else
                    {
                        return FindParent<T>(parent);
                    }
                }
            }

            return default(T);
        } 
    }
}
