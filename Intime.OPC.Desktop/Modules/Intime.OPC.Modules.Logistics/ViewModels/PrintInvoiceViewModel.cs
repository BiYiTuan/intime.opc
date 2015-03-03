using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Infrastructure.Excel;
using Intime.OPC.Infrastructure.Mvvm;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using Intime.OPC.Modules.Logistics.Models;
using Intime.OPC.Modules.Logistics.Print;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Intime.OPC.DataService.Interface.Trans;
using Intime.OPC.DataService.IService;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;
using Intime.OPC.Infrastructure.Events;
using Intime.OPC.Modules.Logistics.Events;

namespace Intime.OPC.Modules.Logistics.ViewModels
{
    [Export("PrintInvoiceViewModel", typeof(PrintInvoiceViewModel))]
    public class PrintInvoiceViewModel : BindableBase
    {
        private SaleDto _opcSale;
        private Invoice4Get invoice4Get;
        private List<OPC_SaleDetail> invoiceDetail4List;
        private IEnumerable<SaleDto> saleList;

        public PrintInvoiceViewModel()
        {
            EnumSetRemarkType = EnumSetRemarkType.SetSaleRemark;
            //初始化命令属性
            CommandSearch = new AsyncDelegateCommand(CommandSearchExecute);
            CommandViewAndPrint = new AsyncDelegateCommand<bool?>(OnSalesOrderPrintPreview,isRecovered => CanCommandExecute());
            CommandOnlyPrint = new AsyncDelegateCommand<bool?>(OnSalesOrderPrint, isRecovered => CanCommandExecute());
            CommandFinish = new AsyncDelegateCommand(CommandFinishExecute, CanCommandExecute);
            CommandSetRemark = new DelegateCommand(CommandRemarkExecute);
            CommandGetDown = new AsyncDelegateCommand(CommandGetDownExecute);
            CommandDbClick = new AsyncDelegateCommand(CommandDbClickExecute);
            CommandExport = new AsyncDelegateCommand(OnExport, CanExportExecute);
            Invoice4Get = new Invoice4Get();
            SearchSaleStatus = EnumSearchSaleStatus.CompletePrintSearchStatus;

            SubscribeSalesOrderDetectedEvents();
        }

        public EnumSearchSaleStatus SearchSaleStatus { get; set; }

        //Grid数据集
        public IEnumerable<SaleDto> SaleList
        {
            get { return saleList; }
            set { SetProperty(ref saleList, value); }
        }

        //界面查询条件
        public Invoice4Get Invoice4Get
        {
            get { return invoice4Get; }
            set { SetProperty(ref invoice4Get, value); }
        }

        //选择上面行数据时赋值的数据集
        public SaleDto SaleSelected
        {
            get { return _opcSale; }
            set { SetProperty(ref _opcSale, value); }
        }

        //销售单明细Grid数据集
        public List<OPC_SaleDetail> InvoiceDetail4List
        {
            get { return invoiceDetail4List; }
            set { SetProperty(ref invoiceDetail4List, value); }
        }

        public EnumSetRemarkType EnumSetRemarkType { get; set; }

        #region Commands

        public ICommand CommandSearch { get; set; }
        public ICommand CommandViewAndPrint { get; set; }
        public ICommand CommandOnlyPrint { get; set; }
        public ICommand CommandFinish { get; set; }
        public ICommand CommandSetRemark { get; set; }
        public ICommand CommandGetDown { get; set; }
        public ICommand CommandDbClick { get; set; }
        public ICommand CommandExport { get; set; }

        #endregion

        protected bool CanCommandExecute()
        {
            return (SaleSelected != null);
        }

        private bool CanExportExecute()
        {
            return SaleList != null && SaleList.Any();
        }

        private void SubscribeSalesOrderDetectedEvents()
        {
            var eventAggregator = AppEx.Container.GetInstance<GlobalEventAggregator>();
            var salesOrderDetectedEvent = eventAggregator.GetEvent<UnhandledSalesOrderDetectedEvent>();

            salesOrderDetectedEvent.Subscribe(OnUnhandledSalesOrderDetected, ThreadOption.UIThread);
        }

        private void OnUnhandledSalesOrderDetected(Invoice4Get queryCriteria)
        {
            Invoice4Get = queryCriteria;
            Query();
        }

        private void CommandDbClickExecute()
        {
            if (SaleSelected != null)
            {
                InvoiceDetail4List =
                    AppEx.Container.GetInstance<ILogisticsService>()
                        .SelectSaleDetail(SaleSelected.SaleOrderNo)
                        .Result.ToList();
            }
        }

        //调用接口打开填写Remark的窗口
        public void CommandRemarkExecute()
        {
            //被选择的对象
            string id = SaleSelected.SaleOrderNo;
            var remarkWin = AppEx.Container.GetInstance<IRemark>();
            remarkWin.ShowRemarkWin(id, EnumSetRemarkType.SetSaleRemark);
        }

        public void CommandSearchExecute()
        {
            Query();
            if (SaleList == null || !SaleList.Any())
            {
                MvvmUtility.ShowMessageAsync("没有符合条件的销售单","警告", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public virtual string GetFilter()
        {
            return string.Format("startdate={0}&enddate={1}&orderno={2}&saleorderno={3}&pageIndex={4}&pageSize={5}",
                Invoice4Get.StartSellDate.ToShortDateString(),
                Invoice4Get.EndSellDate.ToShortDateString(),
                Invoice4Get.OrderNo, Invoice4Get.SaleOrderNo, 1, 50);
        }

        protected virtual void Query()
        {
            PageResult<SaleDto> re = AppEx.Container.GetInstance<ILogisticsService>().Search(GetFilter(), SearchSaleStatus);
            SaleList = re.Result;
            if (InvoiceDetail4List != null) InvoiceDetail4List = new List<OPC_SaleDetail>();
        }

        public virtual void ClearOtherList()
        {
        }

        public virtual void RefreshOther(SaleDto SaleOrderNo)
        {
        }

        public void CommandGetDownExecute()
        {
            if (SaleSelected == null)
            {
                if (invoiceDetail4List == null) return;
                invoiceDetail4List.ToList().Clear();
                ClearOtherList();
                return;
            }
            InvoiceDetail4List =
                AppEx.Container.GetInstance<ILogisticsService>().SelectSaleDetail(SaleSelected.SaleOrderNo).Result.ToList();
            RefreshOther(SaleSelected);
        }

        #region 打印销售单

        private bool PrintCommon(bool previewOnly = false, bool isRecoveredPrint = false)
        {
            const string XsdName = "InvoiceDataSet";
            const string ReportName = "Print//SalesOrder.rdlc";

            if (SaleList == null || SaleSelected == null)
            {
                MvvmUtility.ShowMessageAsync("请选择要打印预览的销售单");
                return false;
            }

            SaleSelected.IsRecoveredPrint = isRecoveredPrint;

            var printModel = new PrintModel
            {
                SaleDT = new List<SaleDto> { SaleSelected }, 
                SaleDetailDT = InvoiceDetail4List,
            };

            MvvmUtility.PerformActionOnUIThread(
            () =>
            {
                IPrint print = new PrintWin();
                print.Print(XsdName, ReportName, printModel, previewOnly);
            });

            return true;
        }

        private void OnSalesOrderPrint(bool? isRecovedPrint)
        {
            if (!PrintCommon(true, isRecovedPrint != null && isRecovedPrint.Value)) return;
            //打印完 发请求设置数据库 
            List<string> selectSaleIds = SaleList.Select(e => e.SaleOrderNo).ToList();
            var iTransService = AppEx.Container.GetInstance<ILogisticsService>();
            iTransService.ExecutePrintSale(selectSaleIds);
        }

        private void OnSalesOrderPrintPreview(bool? isRecovedPrint)
        {
            PrintCommon(false, isRecovedPrint != null && isRecovedPrint.Value);
        }

        #endregion

        /// <summary>
        /// 完成销售单打印
        /// </summary>
        public void CommandFinishExecute()
        {
            if (SaleList == null || SaleSelected == null)
            {
                MvvmUtility.ShowMessageAsync("请选择要设置打印完成状态的销售单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            List<string> selectSaleIds = SaleList.Where(e => e.SaleOrderNo == SaleSelected.SaleOrderNo).Select(e => e.SaleOrderNo).ToList();
            var iTransService = AppEx.Container.GetInstance<ILogisticsService>();
            bool bFalg = iTransService.SetStatusAffirmPrintSaleFinish(selectSaleIds);
            MvvmUtility.ShowMessageAsync(bFalg ? "设置打印销售单完成成功" : "设置打印销售单失败", "提示", MessageBoxButton.OK, bFalg ? MessageBoxImage.Information : MessageBoxImage.Error);
            Query();
        }

        private void OnExport()
        {
            if (SaleList == null || !SaleList.Any())
            {
                MvvmUtility.ShowMessageAsync("没有要导出的销售单");
                return;
            }

            var columnDefinitions = new List<ColumnDefinition<SaleDto>> 
            {
                new ColumnDefinition<SaleDto>("订单号",dto => dto.OrderNo),
                new ColumnDefinition<SaleDto>("销售单号",dto => dto.SaleOrderNo),
                new ColumnDefinition<SaleDto>("渠道订单号",dto => dto.TransNo),
                new ColumnDefinition<SaleDto>("订单渠道",dto => dto.OrderSource),
                new ColumnDefinition<SaleDto>("销售状态",dto => dto.SaleStatus),
                new ColumnDefinition<SaleDto>("销售单状态",dto => dto.StatusName),
                new ColumnDefinition<SaleDto>("收银状态",dto => dto.CashStatusName),
                new ColumnDefinition<SaleDto>("销售时间",dto => dto.SellDate),
                new ColumnDefinition<SaleDto>("销售单金额",dto => dto.SalesAmount),
                new ColumnDefinition<SaleDto>("销售单数量",dto => dto.SalesCount),
                new ColumnDefinition<SaleDto>("门店",dto => dto.StoreName),
                new ColumnDefinition<SaleDto>("收银流水号",dto => dto.CashNum),
                new ColumnDefinition<SaleDto>("收银时间",dto => dto.CashDate)
            };

            ExcelUtility.Export(SaleList, columnDefinitions, "销售单");
        }
    }
}