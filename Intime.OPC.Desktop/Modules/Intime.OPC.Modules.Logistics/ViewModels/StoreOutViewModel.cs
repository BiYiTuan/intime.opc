using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Intime.OPC.Infrastructure.Mvvm;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Modules.Logistics.Criteria;
using Intime.OPC.Modules.Logistics.Enums;
using Intime.OPC.Modules.Logistics.Models;
using Intime.OPC.Modules.Logistics.Print;
using Intime.OPC.Modules.Logistics.Services;
using Intime.OPC.DataService.Interface.Trans;
using Intime.OPC.DataService.IService;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;

namespace Intime.OPC.Modules.Logistics.ViewModels
{
    [Export("StoreOutViewModel", typeof (StoreOutViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StoreOutViewModel : ValidatableBindableBase
    {
        private QuerySalesOrderByComposition _queryCriteriaForDeliveryOrder;
        private QueryDeliveryOrderByComposition _queryCriteriaForExpressReceipt;
        private QueryDeliveryOrderByComposition _queryCriteriaForHandOver;

        private OPC_ShippingSale _selectedDeliveryOrder;
        private ObservableCollection<OPC_ShippingSale> _deliveryOrders;

        private ObservableCollection<OPC_Sale> _salesOrdersForDelivery;
        private ObservableCollection<OPC_ShippingSale> _deliveryOrdersForExpress;
        private IList<Order> _orderOfExpressReceipt;
        private OPC_ShippingSale _selectedDeliveryOrderForExpress;
        private ExpressReceiptCreationDTO _shippingSaleCreateDto;

        private IList<Order> _orderOfHandOver;
        private OPC_ShippingSale _selectedDeliveryOrderForHandOver;
        private ObservableCollection<OPC_ShippingSale> _deliveryOrdersForHandOver;

        public StoreOutViewModel()
        {
            DeliveryOrders = new ObservableCollection<OPC_ShippingSale>();

            QueryCriteriaForDeliveryOrder = new QuerySalesOrderByComposition { Status = EnumSaleOrderStatus.ShipInStorage };
            QueryCriteriaForExpressReceipt = new QueryDeliveryOrderByComposition { Status = EnumSaleOrderStatus.PrintInvoice };
            QueryCriteriaForHandOver = new QueryDeliveryOrderByComposition { Status = EnumSaleOrderStatus.PrintExpress };

            QuerySalesOrderForDeliveryCommand = new AsyncDelegateCommand(OnSalesOrderForDeliveryQuery);
            QueryDeliveryOrderForExpressCommand = new AsyncDelegateCommand(OnDeliveryOrderForExpressQuery);
            QueryDeliveryOrderForHandOverCommand = new AsyncDelegateCommand(OnDeliveryOrderForHandOverQuery);

            CreateDeliveryOrderCommand = new AsyncDelegateCommand(OnDeliveryOrderCreate);
            CreateExpressReceiptCommand = new AsyncDelegateCommand(OnExpressReceiptCreate);

            RemarkOrderCommand = new DelegateCommand<int?>(OnOrderRemark);
            RemarkSalesOrderCommand = new DelegateCommand<int?>(OnSalesOrderRemark);
            RemarkDeliveryOrderCommand = new DelegateCommand<int?>(OnDeliveryOrderRemark);
            
            CompleteHandOverCommand = new DelegateCommand(OnHandOverComplete);
            ShippingViaList = AppEx.Container.GetInstance<ICommonInfo>().GetShipViaList();
            ShippingSaleCreateDto = new ExpressReceiptCreationDTO();

            SelectAllSalesOrderForDeliveryCommand = new DelegateCommand<bool?>(OnAllSalesOrderForDeliverySelect);
            LoadOrderOfExpressReceiptCommand = new DelegateCommand<OPC_ShippingSale>(OnOrderOfExpressReceiptLoad);
            LoadOrderOfHandOverCommand = new DelegateCommand<OPC_ShippingSale>(OnOrderOfHandOverLoad);

            PrintExpressReceiptCommand = new AsyncDelegateCommand<OPC_ShippingSale>(OnExpressReceiptPrint);
            PreviewExpressReceiptCommand = new AsyncDelegateCommand<OPC_ShippingSale>(OnExpressReceiptPreview);
            PreviewDeliveryOrderCommand = new AsyncDelegateCommand<OPC_ShippingSale>(OnDeliveryOrderPreview);
            PrintDeliveryOrderCommand = new AsyncDelegateCommand<OPC_ShippingSale>(OnDeliveryOrderPrint);
        }

        #region Commands

        public ICommand SelectAllSalesOrderForDeliveryCommand { get; set; }
        
        public ICommand QuerySalesOrderForDeliveryCommand { get; set; }
        public ICommand QueryDeliveryOrderForExpressCommand { get; set; }
        public ICommand QueryDeliveryOrderForHandOverCommand { get; set; }

        public ICommand PrintDeliveryOrderCommand { get; set; }
        public ICommand PreviewDeliveryOrderCommand { get; set; }
        public ICommand PreviewExpressReceiptCommand { get; set; }
        public ICommand PrintExpressReceiptCommand { get; set; }
        public ICommand CompleteHandOverCommand { get; set; }
        public ICommand CreateExpressReceiptCommand { get; set; }
        public ICommand CreateDeliveryOrderCommand { get; set; }

        public ICommand RemarkOrderCommand { get; set; }
        public ICommand RemarkSalesOrderCommand { get; set; }
        public ICommand RemarkDeliveryOrderCommand { get; set; }

        public ICommand GetListShipSaleCommand { get; set; }
        public ICommand LoadOrderOfExpressReceiptCommand { get; set; }
        public ICommand LoadOrderOfHandOverCommand { get; set; }

        #endregion

        #region Properties

        [Import]
        public IService<OPC_Sale> SalesOrderService { get; set; }

        [Import]
        public IDeliveryOrderService DeliveryOrderService { get; set; }

        [Import]
        public IService<Order> OrderService { get; set; }

        [Import]
        public IService<OPC_SaleDetail> SalesOrderDetailService { get; set; }

        public IRemark RemarkService
        {
            get { return AppEx.Container.GetInstance<IRemark>(); }
        }

        public QuerySalesOrderByComposition QueryCriteriaForDeliveryOrder
        {
            get { return _queryCriteriaForDeliveryOrder; }
            set { SetProperty(ref _queryCriteriaForDeliveryOrder, value); }
        }

        public QueryDeliveryOrderByComposition QueryCriteriaForExpressReceipt
        {
            get { return _queryCriteriaForExpressReceipt; }
            set { SetProperty(ref _queryCriteriaForExpressReceipt, value); }
        }

        public QueryDeliveryOrderByComposition QueryCriteriaForHandOver
        {
            get { return _queryCriteriaForHandOver; }
            set { SetProperty(ref _queryCriteriaForHandOver, value); }
        }

        #region Delivery properties

        public ObservableCollection<OPC_ShippingSale> DeliveryOrders
        {
            get { return _deliveryOrders; }
            set { SetProperty(ref _deliveryOrders, value); }
        }

        public ObservableCollection<OPC_Sale> SalesOrdersForDelivery
        {
            get { return _salesOrdersForDelivery; }
            set { SetProperty(ref _salesOrdersForDelivery, value); }
        }

        public OPC_ShippingSale SelectedDeliveryOrder
        {
            get { return _selectedDeliveryOrder; }
            set { SetProperty(ref _selectedDeliveryOrder, value); }
        }

        public OPC_Sale SelectedSalesOrder { get; set; }

        #endregion

        #region Express properties

        public ObservableCollection<OPC_ShippingSale> DeliveryOrdersForExpress
        {
            get { return _deliveryOrdersForExpress; }
            set { SetProperty(ref _deliveryOrdersForExpress, value); }
        }

        public OPC_ShippingSale SelectedDeliveryOrderForExpress
        {
            get { return _selectedDeliveryOrderForExpress; }
            set { SetProperty(ref _selectedDeliveryOrderForExpress, value); }
        }

        public IList<Order> OrderOfExpressReceipt
        {
            get { return _orderOfExpressReceipt; }
            set { SetProperty(ref _orderOfExpressReceipt, value); }
        }

        public IList<ShipVia> ShippingViaList { get; set; }

        public ExpressReceiptCreationDTO ShippingSaleCreateDto
        {
            get { return _shippingSaleCreateDto; }
            set { SetProperty(ref _shippingSaleCreateDto, value); }
        }

        #endregion

        #region HandOver properties

        public OPC_ShippingSale SelectedDeliveryOrderForHandOver
        {
            get { return _selectedDeliveryOrderForHandOver; }
            set { SetProperty(ref _selectedDeliveryOrderForHandOver, value); }
        }

        public ObservableCollection<OPC_ShippingSale> DeliveryOrdersForHandOver
        {
            get { return _deliveryOrdersForHandOver; }
            set { SetProperty(ref _deliveryOrdersForHandOver, value); }
        }

        public IList<Order> OrderOfHandOver
        {
            get { return _orderOfHandOver; }
            set { SetProperty(ref _orderOfHandOver, value); }
        }

        #endregion

        #endregion

        #region Command Handlers

        /// <summary>
        /// 全选所有销售单
        /// </summary>
        /// <param name="isSelected"></param>
        private void OnAllSalesOrderForDeliverySelect(bool? isSelected)
        {
            if (SalesOrdersForDelivery == null || !SalesOrdersForDelivery.Any()) return;

            SalesOrdersForDelivery.ForEach(salesOrder => salesOrder.IsSelected = isSelected.Value);
        }

        /// <summary>
        /// 查询需要生成发货单的销售单
        /// </summary>
        private void OnSalesOrderForDeliveryQuery()
        {
            var salesOrders = SalesOrderService.QueryAll(_queryCriteriaForDeliveryOrder);
            SalesOrdersForDelivery = salesOrders.ToObservableCollection();
            if (salesOrders.Count == 0)
            {
                MvvmUtility.ShowMessageAsync("没有符合条件的销售单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OnOrderOfExpressReceiptLoad(OPC_ShippingSale deliveryOrder)
        {
            if (deliveryOrder == null)
            {
                OrderOfExpressReceipt = null;
                return;
            }
            var salesOrder = deliveryOrder.SalesOrders.FirstOrDefault();
            OrderOfExpressReceipt = salesOrder == null ? null : new List<Order> { salesOrder.Order };
        }

        private void OnOrderOfHandOverLoad(OPC_ShippingSale deliveryOrder)
        {
            if (deliveryOrder == null)
            {
                OrderOfHandOver = null;
                return;
            }
            var salesOrder = deliveryOrder.SalesOrders.FirstOrDefault();
            OrderOfHandOver = salesOrder == null ? null : new List<Order> { salesOrder.Order };
        }

        /// <summary>
        /// 查询需要生成快递单的发货单
        /// </summary>
        private void OnDeliveryOrderForExpressQuery()
        {
            var deliveryOrders = DeliveryOrderService.QueryAll(_queryCriteriaForExpressReceipt);
            DeliveryOrdersForExpress = deliveryOrders.ToObservableCollection();
            if (deliveryOrders.Count == 0)
            {
                MvvmUtility.ShowMessageAsync("没有符合条件的发货单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// 生成发货单
        /// </summary>
        private void OnDeliveryOrderCreate()
        {
            if (SalesOrdersForDelivery == null || !SalesOrdersForDelivery.Any())
            {
                MvvmUtility.ShowMessageAsync("请选择至少一个销售单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedSalesOrdersForDelivery = SalesOrdersForDelivery.Where(salesOrder => salesOrder.IsSelected);
            if (!selectedSalesOrdersForDelivery.Any())
            {
                MvvmUtility.ShowMessageAsync("请选择至少一个销售单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (selectedSalesOrdersForDelivery.Distinct(salesOrder => salesOrder.Order.OrderNo).Count() > 1)
            {
                MvvmUtility.ShowMessageAsync("所选择的销售单必须来自同一订单，否则无法生成发货单", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (selectedSalesOrdersForDelivery.Distinct(salesOrder => salesOrder.Order.CustomerName).Count() > 1 
                ||selectedSalesOrdersForDelivery.Distinct(salesOrder => salesOrder.Order.CustomerAddress).Count() > 1
                ||selectedSalesOrdersForDelivery.Distinct(salesOrder => salesOrder.Order.CustomerPhone).Count() > 1)
            {
                MvvmUtility.ShowMessageAsync("所选择的销售单收货信息不一致，无法生成发货单", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var firstSalesOrder = selectedSalesOrdersForDelivery.First();

            var deliveryOrderCreationDto = new DeliveryOrderCreationDTO() 
            { 
                SalesOrderNos = selectedSalesOrdersForDelivery.Select(salesOrder => salesOrder.SaleOrderNo).ToArray() 
            };

            SalesOrdersForDelivery.SafelyRemove(salesOrder => salesOrder.IsSelected);

            var deliveryOrder = DeliveryOrderService.Create(deliveryOrderCreationDto);
            DeliveryOrders.SafelyInsert(0, deliveryOrder);
        }

        /// <summary>
        /// 生成快递单
        /// </summary>
        private void OnExpressReceiptCreate()
        {
            if (SelectedDeliveryOrderForExpress == null)
            {
                MvvmUtility.ShowMessageAsync("请选择一张发货单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ShippingSaleCreateDto.ValidateProperties();
            if (ShippingSaleCreateDto.HasErrors) return;

            ShippingSaleCreateDto.DeliveryOrderId = SelectedDeliveryOrderForExpress.Id;
            DeliveryOrderService.Update<ExpressReceiptCreationDTO>(SelectedDeliveryOrderForExpress, ShippingSaleCreateDto);

            SelectedDeliveryOrderForExpress.ShipViaId = ShippingSaleCreateDto.ShipViaID;
            SelectedDeliveryOrderForExpress.ExpressCode = ShippingSaleCreateDto.ShippingNo;
            SelectedDeliveryOrderForExpress.ShipViaExpressFee = ShippingSaleCreateDto.ShippingFee;
            SelectedDeliveryOrderForExpress.ShipCompanyName = ShippingViaList.Where(shippingVia => shippingVia.Id == ShippingSaleCreateDto.ShipViaID).First().Name;
            
            ShippingSaleCreateDto.ShippingNo = null;
        }

        /// <summary>
        /// 完成发货交接
        /// </summary>
        private void OnHandOverComplete()
        {
            if (SelectedDeliveryOrderForHandOver == null)
            {
                MvvmUtility.ShowMessageAsync("请选择一张快递单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DeliveryOrderService.CompleteHandOver(SelectedDeliveryOrderForHandOver);
            var selectedDeliveryOrderID = SelectedDeliveryOrderForHandOver.Id;
            DeliveryOrdersForHandOver.SafelyRemove(deliveryOrder => deliveryOrder.Id == selectedDeliveryOrderID);
        }

        /// <summary>
        /// 发货单备注
        /// </summary>
        private void OnDeliveryOrderRemark(int? id)
        {
            RemarkService.ShowRemarkWin(id.Value.ToString(), EnumSetRemarkType.SetShipSaleRemark); 
        }

        /// <summary>
        /// 销售单备注
        /// </summary>
        private void OnSalesOrderRemark(int? id)
        {
            RemarkService.ShowRemarkWin(id.Value.ToString(), EnumSetRemarkType.SetSaleRemark);
        }

        /// <summary>
        /// 订单备注
        /// </summary>
        private void OnOrderRemark(int? id)
        {
            RemarkService.ShowRemarkWin(id.Value.ToString(), EnumSetRemarkType.SetOrderRemark);
        }

        /// <summary>
        /// 查询快递单
        /// </summary>
        private void OnDeliveryOrderForHandOverQuery()
        {
            var deliveryOrders = DeliveryOrderService.QueryAll(_queryCriteriaForHandOver);
            DeliveryOrdersForHandOver = deliveryOrders.ToObservableCollection();
            if (deliveryOrders.Count == 0)
            {
                MvvmUtility.ShowMessageAsync("没有符合条件的快递单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #region 打印发货单

        private void OnDeliveryOrderPrint(OPC_ShippingSale deliveryOrder)
        {
            PrintDeliveryOrder(deliveryOrder, false);
        }

        private void OnDeliveryOrderPreview(OPC_ShippingSale deliveryOrder)
        {
            PrintDeliveryOrder(deliveryOrder, true);
        }

        private void PrintDeliveryOrder(OPC_ShippingSale deliveryOrder, bool preview)
        {
            const string ReportName = "Print//DeliveryReceipt.rdlc";

            if (deliveryOrder == null)
            {
                MvvmUtility.ShowMessageAsync("请选择一张发货单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!deliveryOrder.SalesOrders.Any())
            {
                MvvmUtility.ShowMessageAsync("没有对应的销售单信息", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var order = deliveryOrder.SalesOrders.First().Order;
            var salesOrders = deliveryOrder.SalesOrders;
            var salesOrderDetails = new List<OPC_SaleDetail>();
            salesOrders.ForEach(salesOrder => 
            {
                var details = SalesOrderDetailService.QueryAll(new QuerySalesOrderDetailBySalesOrderNo { SalesOrderNo = salesOrder.SaleOrderNo });
                salesOrderDetails.AddRange(details);
            });

            Action print = () =>
            {
                IPrint reporter = new PrintWin();
                reporter.PrintDeliveryOrder(ReportName, order, salesOrders, salesOrderDetails, !preview);
            };
            
            MvvmUtility.PerformActionOnUIThread(print);

            if (!preview) DeliveryOrderService.Print(deliveryOrder, ReceiptType.DeliveryOrder);
        }

        #endregion

        #region 打印快递单

        /// <summary>
        /// 打印预览快递单
        /// </summary>
        private void OnExpressReceiptPreview(OPC_ShippingSale deliveryOrder)
        {
            PrintExpressReceipt(deliveryOrder, true);
        }

        /// <summary>
        /// 打印快递单
        /// </summary>
        private void OnExpressReceiptPrint(OPC_ShippingSale deliveryOrder)
        {
            PrintExpressReceipt(deliveryOrder, false);
        }

        /// <summary>
        /// 打印快递单
        /// </summary>
        /// <param name="deliveryOrder"></param>
        /// <param name="preview"></param>
        private void PrintExpressReceipt(OPC_ShippingSale deliveryOrder, bool preview)
        {
            if (deliveryOrder == null)
            {
                MvvmUtility.ShowMessageAsync("请选择一张快递单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!deliveryOrder.SalesOrders.Any())
            {
                MvvmUtility.ShowMessageAsync("没有对应的销售单信息", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (deliveryOrder.ShipViaId == null)
            {
                MvvmUtility.ShowMessageAsync("尚未生成快递单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var selectedShippingVia = ShippingViaList.Where(shippingVia => shippingVia.Id == deliveryOrder.ShipViaId.Value).FirstOrDefault();
            if (selectedShippingVia == null)
            {
                MvvmUtility.ShowMessageAsync("无效的快递公司", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(selectedShippingVia.TemplateName))
            {
                MvvmUtility.ShowMessageAsync("暂不支持此快递公司的打印", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var reportName = string.Format("Print//{0}.rdlc", selectedShippingVia.TemplateName);
            var salesOrder = deliveryOrder.SalesOrders.First();
            var order = salesOrder.Order;
            var expressReceiptPrintModel = new PrintExpressModel()
            {
                CustomerAddress = order.CustomerAddress,
                CustomerName = order.CustomerName,
                CustomerPhone = order.CustomerPhone,
                StoreName = salesOrder.StoreName,
                StoreAddress = salesOrder.StoreAddress,
                StoreTel = salesOrder.StoreTelephone,
                ExpressFee = deliveryOrder.ExpressFee.ToString("f2")
            };

            Action print = () => 
            {
                IPrint reporter = new PrintWin();
                reporter.PrintExpress(reportName, expressReceiptPrintModel, !preview);
            };
            
            MvvmUtility.PerformActionOnUIThread(print);

            if (!preview) DeliveryOrderService.Print(deliveryOrder, ReceiptType.ExpressReceipt);
        }

        #endregion

        #endregion
    }
}