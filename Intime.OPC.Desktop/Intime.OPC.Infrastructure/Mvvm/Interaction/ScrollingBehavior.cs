using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace Intime.OPC.Infrastructure.Mvvm
{
    public class ScrollingBehavior
    {        
        #region Dependency Properties

        public static readonly DependencyProperty IsEnabledProperty;

        public static readonly DependencyProperty ScrollToBottomCommandProperty;

        public static readonly DependencyProperty ScrollToTopCommandProperty;

        public static void SetIsEnabled(DependencyObject DepObject, string value)
        {
            DepObject.SetValue(IsEnabledProperty, value);
        }

        public static bool GetIsEnabled(DependencyObject DepObject)
        {
            return (bool)DepObject.GetValue(IsEnabledProperty);
        }

        public static void SetScrollToBottomCommand(DependencyObject DepObject, ICommand value)
        {
            DepObject.SetValue(ScrollToBottomCommandProperty, value);
        }

        public static ICommand GetScrollToBottomCommand(DependencyObject DepObject)
        {
            var command = DepObject.GetValue(ScrollToBottomCommandProperty);
            return command == null ? null : (ICommand)(command);
        }

        public static void SetScrollToTopCommand(DependencyObject DepObject, ICommand value)
        {
            DepObject.SetValue(ScrollToTopCommandProperty, value);
        }

        public static ICommand GetScrollToTopCommand(DependencyObject DepObject)
        {
            var command = DepObject.GetValue(ScrollToTopCommandProperty);
            return command == null ? null: (ICommand)(command);
        }

        #endregion

        static ScrollingBehavior()
        {
            IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled",
                                                                typeof(bool),
                                                                typeof(ScrollingBehavior),
                                                                new UIPropertyMetadata(false, IsFrontTurn));

            ScrollToBottomCommandProperty = DependencyProperty.RegisterAttached("ScrollToBottomCommand",
                                                                typeof(ICommand),
                                                                typeof(ScrollingBehavior));

            ScrollToTopCommandProperty = DependencyProperty.RegisterAttached("ScrollToTopCommand",
                                                    typeof(ICommand),
                                                    typeof(ScrollingBehavior));
        }

        private static void IsFrontTurn(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var frameworkElement = sender as FrameworkElement;
            if (frameworkElement == null) return;

            if (e.NewValue is bool && ((bool)e.NewValue) == true)
            {
                frameworkElement.Loaded += new RoutedEventHandler(OnElementLoaded);
            }
            else 
            {
                frameworkElement.Loaded -= new RoutedEventHandler(OnElementLoaded);
            }
        }

        private static void OnElementLoaded(object sender, RoutedEventArgs e)
        {
            var scrollViewer = GetFirstChildOfType<ScrollViewer>((FrameworkElement)sender);

            if (scrollViewer != null)
            {
                scrollViewer.ScrollChanged -= OnScrollChanged;
                scrollViewer.ScrollChanged += OnScrollChanged;
            }
        }

        #region GetFirstChildOfType

        private static T GetFirstChildOfType<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            if (dependencyObject == null) return null;

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
            {
                var child = VisualTreeHelper.GetChild(dependencyObject, i);
                var result = (child as T) ?? GetFirstChildOfType<T>(child);
                if (result != null) return result;
            }

            return null;
        }

        private static DependencyObject GetScrollViewOwner(DependencyObject dependencyObject)
        {
            while (true)
            {
                if (dependencyObject == null) break;

                var command = dependencyObject.GetValue(ScrollToBottomCommandProperty);
                if (command != null) return dependencyObject;

                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }
            
            return null;
        }

        #endregion

        private const int PreservedOffset = 10;

        private static void OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;
            if (scrollViewer == null) return;

            var target = GetScrollViewOwner(scrollViewer);
            if (target == null) return;

            if (e.VerticalChange > 0 && scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
            {
                ICommand command = GetScrollToBottomCommand(target);
                if (command != null) command.Execute(target);
            }

            if (scrollViewer.VerticalOffset == 0 && e.VerticalChange < 0)
            {
                ICommand command = GetScrollToTopCommand(target);
                if (command != null) command.Execute(target);
            }
        }
    }
}