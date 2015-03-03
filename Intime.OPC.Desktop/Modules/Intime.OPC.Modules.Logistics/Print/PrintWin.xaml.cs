using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing.Printing;
using System.Windows;
using Intime.OPC.Infrastructure.Print;
using Microsoft.Reporting.WinForms;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure.Mvvm.Utility;

namespace Intime.OPC.Modules.Logistics.Print
{
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    [Export(typeof (IPrint))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class PrintWin : Window, IPrint
    {
        public PrintWin()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     打印接口实现
        /// </summary>
        /// <param name="xsdName">创建的数据源名称(xsd文件的名称)</param>
        /// <param name="rdlcName">报表模板的路径</param>
        /// <param name="dataName">建立数据源时起的名字</param>
        /// <param name="dt">数据集dt</param>
        public void Print(string xsdName, string rdlcName, PrintModel dtList, bool isPrint=false)
        {
            try
            {
                var myRptDS = new ReportDataSource();
                myRptDS = new ReportDataSource(xsdName, dtList.SaleDetailDT); //创建的数据源名称(xsd文件的名称),数据集
                myRptDS.Name = "SaleDetailDT";
                _reportViewer.LocalReport.DataSources.Add(myRptDS);


                myRptDS = new ReportDataSource(xsdName, dtList.SaleDT); //创建的数据源名称(xsd文件的名称),数据集
                myRptDS.Name = "SaleDT";
                _reportViewer.LocalReport.DataSources.Add(myRptDS);

                myRptDS = new ReportDataSource(xsdName, dtList.OrderDT); //创建的数据源名称(xsd文件的名称),数据集
                myRptDS.Name = "OrderDT";
                _reportViewer.LocalReport.DataSources.Add(myRptDS);

                _reportViewer.LocalReport.ReportPath = rdlcName; //报表的地址

                if (isPrint)
                {
                    Print(_reportViewer.LocalReport);
                }
                else
                {
                    _reportViewer.RefreshReport();
                    ShowDialog();
                }
            }
            catch (Exception Ex)
            {
                MvvmUtility.ShowMessageAsync(Ex.Message,"错误");
            }
        }

        public void PrintExpress(string rdlcName, PrintExpressModel printExpressModel, bool isPrint = false)
        {
            try
            { 
                var myRptDs = new ReportDataSource("PrintExpressModel",new List<PrintExpressModel>{ printExpressModel});
                _reportViewer.LocalReport.DataSources.Add(myRptDs);
                _reportViewer.LocalReport.ReportPath = rdlcName;
                if (isPrint)
                {
                    Print(_reportViewer.LocalReport);
                }
                else
                {
                    _reportViewer.RefreshReport();
                    ShowDialog();
                }
            }
            catch (Exception Ex)
            {
                MvvmUtility.ShowMessageAsync(Ex.Message,"错误");
            }
        }

        public void PrintDeliveryOrder(string rdlcName, Order order, IList<OPC_Sale> opcSales, IList<OPC_SaleDetail> listOpcSaleDetails, bool isPrint = false)
        {
            try
            {
                var myRptDs = new ReportDataSource("FHD", opcSales);
                _reportViewer.LocalReport.DataSources.Add(myRptDs);

                myRptDs = new ReportDataSource("OrderDT", new List<Order>() { order });
                _reportViewer.LocalReport.DataSources.Add(myRptDs);

                myRptDs = new ReportDataSource("SaleDetailDT", listOpcSaleDetails);
                _reportViewer.LocalReport.DataSources.Add(myRptDs);
                _reportViewer.LocalReport.ReportPath = rdlcName;
                if (isPrint)
                {
                    Print(_reportViewer.LocalReport);
                }
                else
                {
                    _reportViewer.RefreshReport();
                    ShowDialog();
                }
            }
            catch (Exception Ex)
            {
                MvvmUtility.ShowMessageAsync(Ex.Message,"错误",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Print(LocalReport report)
        {
            ReportPrintDocument printDocument = new ReportPrintDocument(report);
            printDocument.Print();
        }
    }
}