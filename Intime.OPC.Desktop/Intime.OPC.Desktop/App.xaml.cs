using Intime.OPC.Infrastructure;
using Intime.OPC.Infrastructure.ErrorHandling;
using Intime.OPC.Infrastructure.Events;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Intime.OPC.Desktop
{
    using Intime.OPC.Infrastructure.Job;

    /// <summary>
    /// App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //Wire up exception events
            WireupUnhandledExceptionEvents();

            //Configure log4net
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo("Config/log4net.config"));

            Bootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }

        /// <summary>
        /// Initialize error handling by wiring up event handlers
        /// </summary>
        private void WireupUnhandledExceptionEvents()
        {
            //Wire up event handler to capture unhandled exceptions
            DispatcherUnhandledException += OnDispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            PublishExceptionEvent(exception);
        }

        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            PublishExceptionEvent(e.Exception);
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            PublishExceptionEvent(e.Exception, () => e.Handled = true);
        }

        private void PublishExceptionEvent(Exception ex, Action completeCallback = null)
        {
            var eventAggregator = AppEx.Container.GetInstance<GlobalEventAggregator>();
            var raiseExceptionEvent = eventAggregator.GetEvent<RaiseExceptionEvent>();
            raiseExceptionEvent.Publish(new ExceptionTriad { Exception = ex, CompleteCallback = completeCallback });
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            AppEx.Container.GetInstance<JobEngine>().Shutdown();
        }
    }
}