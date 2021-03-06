﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Intime.OPC.Infrastructure.Mvvm;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Intime.OPC.DataService.Interface.Customer;
using Intime.OPC.DataService.Interface.Trans;
using Intime.OPC.DataService.IService;
using Intime.OPC.Domain.Customer;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;

namespace Intime.OPC.Modules.CustomerService.ViewModels
{
    [Export(typeof(CustomerStockoutRemindCommonViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CustomerStockoutRemindCommonViewModel : BindableBase
    {

        public CustomerStockoutRemindCommonViewModel()
        {
            CommandGetOrder = new AsyncDelegateCommand(GetOrder);
            CommandGetSaleByOrderId = new AsyncDelegateCommand(GetSaleByOrderId);
            CommandGetSaleDetailBySaleId = new AsyncDelegateCommand(GetSaleDetailBySaleId);
            CommandCannotReplenish = new DelegateCommand(CannotReplenish);
            InitCombo();
            OutOfStockNotifyRequest = new OutOfStockNotifyRequest();
        }

        public DelegateCommand CommandSetRemark { get; set; }

        public IList<KeyValue> StoreList { get; set; }

        public IList<KeyValue> PaymentTypeList { get; set; }

        public IList<KeyValue> OrderStatusList { get; set; }

        public IList<KeyValue> OutGoodsTypeList { get; set; }

        public ICommand CommandGetOrder { get; set; }
        public ICommand CommandGetSaleByOrderId { get; set; }
        public ICommand CommandGetSaleDetailBySaleId { get; set; }
        public ICommand CommandGetShipping { get; set; }
        public ICommand CommandGetOrderByShippingId { get; set; }
        public ICommand CommandGetSaleByOrderNoShipping { get; set; }
        public ICommand CommandCannotReplenish { get; set; }

        #region Tab1页签

        //Tab1选中的Order中的数据集
        private OutOfStockNotifyRequest _outOfStockNotifyRequest;
        private List<Order> _orderList;
        private List<OPC_SaleDetail> _saleDetailList;
        private List<OPC_Sale> _saleList;
        private Order _selectOrder;

        //Tab1选中的Sale中的数据集
        private OPC_Sale _selectSale;

        public Order SelectOrder
        {
            get { return _selectOrder; }
            set { SetProperty(ref _selectOrder, value); }
        }

        public OPC_Sale SelectSale
        {
            get { return _selectSale; }
            set { SetProperty(ref _selectSale, value); }
        }

        //Tab1中Grid数据集1

        public List<Order> OrderList
        {
            get { return _orderList; }
            set { SetProperty(ref _orderList, value); }
        }

        //Tab1中Grid数据集2

        public List<OPC_Sale> SaleList
        {
            get { return _saleList; }
            set { SetProperty(ref _saleList, value); }
        }

        //Tab1中Grid数据集3

        public List<OPC_SaleDetail> SaleDetailList
        {
            get { return _saleDetailList; }
            set { SetProperty(ref _saleDetailList, value); }
        }

        //界面查询条件

        public OutOfStockNotifyRequest OutOfStockNotifyRequest
        {
            get { return _outOfStockNotifyRequest; }
            set { SetProperty(ref _outOfStockNotifyRequest, value); }
        }
        private async void CannotReplenish()
        {
            if (SaleList == null || !SaleList.Any())
            {
                await MvvmUtility.ShowMessageAsync("请选择销售单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var saleListSelected = SaleList.Where(e => e.IsSelected).ToList();
            var succeeded = AppEx.Container.GetInstance<ICustomerInquiryService>().SetCannotReplenish(saleListSelected.Select(e => e.SaleOrderNo).ToList());
            await MvvmUtility.ShowMessageAsync(succeeded ? "设置取销售单成功" : "设置取消销售单失败", "提示", MessageBoxButton.OK, succeeded ? MessageBoxImage.Information : MessageBoxImage.Error);
            if (succeeded)
            {
                SaleList = new List<OPC_Sale>();
                SaleDetailList = new List<OPC_SaleDetail>();
                GetSaleByOrderId();
            }
        }

        public void GetOrder()
        {
            OrderList = new List<Order>();
            var re = AppEx.Container.GetInstance<ICustomerInquiryService>().GetOrderStockout(OutOfStockNotifyRequest.ToString()).Result;
            if (re != null)
            {
                OrderList = re.ToList();
            }

            MvvmUtility.WarnIfEmpty(OrderList, "订单");
        }

        public void GetSaleByOrderId()
        {
            if (SelectOrder == null || string.IsNullOrEmpty(SelectOrder.Id.ToString()))
            {
                return;
            }
            string orderNo = string.Format("orderID={0}&pageIndex={1}&pageSize={2}", SelectOrder.OrderNo, 1, 300);
            //这个工作状态
            SaleList = AppEx.Container.GetInstance<ICustomerInquiryService>().GetSaleByOrderNo(orderNo).Result.ToList();
            if (SaleList != null && SaleList.Any())
            {
                OPC_Sale sale = SaleList.ToList()[0];
                SaleDetailList = AppEx.Container.GetInstance<ILogisticsService>().SelectSaleDetail(sale.SaleOrderNo).Result.ToList();
            }
        }

        public void GetSaleDetailBySaleId()
        {
            if (SelectSale == null || string.IsNullOrEmpty(SelectSale.Id.ToString()))
            {
                return;
            }
            string saleOrderNo = SelectSale.SaleOrderNo;
            //这个工作状态
            SaleDetailList = AppEx.Container.GetInstance<ILogisticsService>().SelectSaleDetail(saleOrderNo).Result.ToList();
        }

        #endregion


        public void SetRemarkOrder()
        {
            //被选择的对象
            string id = SelectOrder.OrderNo;
            var remarkWin = AppEx.Container.GetInstance<IRemark>();
            remarkWin.ShowRemarkWin(id, EnumSetRemarkType.SetOrderRemark);
        }

        public void InitCombo()
        {
            // OderStatusList=new 
            StoreList = AppEx.Container.GetInstance<ICommonInfo>().GetStoreList();
            OrderStatusList = AppEx.Container.GetInstance<ICommonInfo>().GetOrderStatus();
            PaymentTypeList = AppEx.Container.GetInstance<ICommonInfo>().GetPayMethod();
            OutGoodsTypeList = AppEx.Container.GetInstance<ICommonInfo>().GetSaleOrderStatus();
        }
    }

}

