//===================================================================================
// Microsoft patterns & practices
// Composite Application Guidance for Windows Presentation Foundation
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================

//.Regions;

using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using OPCApp.Infrastructure;
using Intime.OPC.Modules.Logistics.ViewModels;

//.Infrastructure;

namespace Intime.OPC.Modules.Logistics.Views
{
    [Export]
    public partial class NavigationItemView : UserControl, IPartImportsSatisfiedNotification
    {
        private static readonly Uri PrintInvoiceUri = new Uri("PrintInvoice", UriKind.Relative);
        private static readonly Uri StoreInUri = new Uri("StoreIn", UriKind.Relative);
        private static readonly Uri StoreOutUri = new Uri("StoreOut", UriKind.Relative);

        public NavigationItemViewModel aniv = new NavigationItemViewModel();
        [Import] public IRegionManager regionManager;

        public NavigationItemView()
        {
            InitializeComponent();
            aniv.PrintInvoiceCommand = new DelegateCommand(PrintInvoiceCommand);
            aniv.StoreInCommand = new DelegateCommand(StoreInCommand);
            aniv.StoreOutCommand = new DelegateCommand(StoreOutCommand);
            DataContext = aniv;
        }

        void IPartImportsSatisfiedNotification.OnImportsSatisfied()
        {
            IRegion mainContentRegion = regionManager.Regions[RegionNames.MainContentRegion];
            if (mainContentRegion != null && mainContentRegion.NavigationService != null)
            {
                mainContentRegion.NavigationService.Navigated += MainContentRegion_Navigated;
            }
        }

        public void MainContentRegion_Navigated(object sender, RegionNavigationEventArgs e)
        {
            UpdateNavigationButtonState(e.Uri);
        }

        private void UpdateNavigationButtonState(Uri uri)
        {
            //this.NavigateToCalendarRadioButton.IsChecked = (uri == calendarViewUri);
        }

        private void PrintInvoiceCommand()
        {
            regionManager.RequestNavigate(RegionNames.MainContentRegion, PrintInvoiceUri);
        }

        private void StoreInCommand()
        {
            regionManager.RequestNavigate(RegionNames.MainContentRegion, StoreInUri);
        }

        private void StoreOutCommand()
        {
            regionManager.RequestNavigate(RegionNames.MainContentRegion, StoreOutUri);
        }
    }
}