using System;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using MahApps.Metro.Controls;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using Intime.OPC.Infrastructure;

namespace Intime.OPC.Desktop
{
    public class Bootstrapper : MefBootstrapper
    {
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof (Bootstrapper).Assembly));

            AggregateCatalog.Catalogs.Add(new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory));
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<MetroWindow>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            AppEx.Init(Container);

            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.ShowDialog();
            Application.Current.Shutdown();
        }
    }
}