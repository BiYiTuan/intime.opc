using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Intime.OPC.DataService.Interface.Trans;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;
using Intime.OPC.Infrastructure.Mvvm;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using Intime.OPC.Modules.Logistics.Services;

namespace Intime.OPC.Modules.Logistics.ViewModels
{
    [Export("StoreInViewModel", typeof (StoreInViewModel))]
    public class StoreInViewModel : PrintInvoiceViewModel
    {
        [Import]
        public ISalesOrderService SalesOrderService { get; set; }

        public StoreInViewModel()
        {
            SearchSaleStatus = EnumSearchSaleStatus.StoreInDataBaseSearchStatus;
            SellOutCommand = new AsyncDelegateCommand(OnSellOut, CanCommandExecute);
            InStockCommand = new AsyncDelegateCommand(OnInStock, CanCommandExecute);
            ApplyPaymentNumberCommand = new AsyncDelegateCommand(OnPaymentNumberApply,CanPaymentNumberApply);
        }

        #region Commands

        public ICommand SellOutCommand { get; set; }
        public ICommand InStockCommand { get; set; }
        public ICommand ApplyPaymentNumberCommand { get; set; }

        #endregion 

        private bool CanPaymentNumberApply()
        {
            return SaleSelected != null && SaleSelected.OrderProductType == SalesOrderType.MiniIntime;
        }

        /// <summary>
        /// 补录收银流水号
        /// </summary>
        private async void OnPaymentNumberApply()
        {
            //如果是迷你银商品
            var cashNumber = await MvvmUtility.ShowInputDialogAsync("补录收银流水号", "收银流水号", SaleSelected.CashNum);
            //用户点击取消按钮
            if (cashNumber == null) return;

            SalesOrderService.ReceivePayment(new OPC_Sale { SaleOrderNo = SaleSelected.SaleOrderNo }, cashNumber);
            SaleSelected.CashNum = cashNumber;
        }

        /// <summary>
        /// 设置缺货状态
        /// </summary>
        private void OnSellOut()
        {
            List<string> salesOrderIds = SaleList.Where(n => n.SaleOrderNo == SaleSelected.SaleOrderNo ).Select(e => e.SaleOrderNo).ToList();
            var service = AppEx.Container.GetInstance<ILogisticsService>();
            bool succeeded = service.SetStatusSoldOut(salesOrderIds);
            MvvmUtility.ShowMessageAsync(succeeded ? "设置缺货成功" : "设置缺货失败", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            Query();
        }

        /// <summary>
        /// 物流入库确认
        /// </summary>
        private void OnInStock()
        {
            List<string> salesOrderIds = SaleList.Where(salesOrder => salesOrder.SaleOrderNo == SaleSelected.SaleOrderNo
                && ((salesOrder.OrderProductType == SalesOrderType.System && salesOrder.Status == EnumSaleOrderStatus.ShoppingGuidePickUp)
                || (salesOrder.OrderProductType == SalesOrderType.MiniIntime && !string.IsNullOrEmpty(salesOrder.CashNum)))).Select(e => e.SaleOrderNo).ToList();
            if (salesOrderIds.Count == 0)
            {
                MvvmUtility.ShowMessageAsync("销售单无法办理入库,系统商品必须是“导购已提货”，迷你银商品必须已提供补录的收银流水号", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var service = AppEx.Container.GetInstance<ILogisticsService>();
            bool succeeded = service.SetStatusStoreInSure(salesOrderIds);
            MvvmUtility.ShowMessageAsync(succeeded ? "销售单入库成功" : "销售单入库失败", "提示", MessageBoxButton.OK, succeeded ? MessageBoxImage.Information : MessageBoxImage.Error);

            Query();
        }
    }
}