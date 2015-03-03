using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Intime.OPC.Infrastructure.Mvvm;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Intime.OPC.DataService.Interface.RMA;
using Intime.OPC.DataService.Interface.Trans;
using Intime.OPC.DataService.IService;
using Intime.OPC.Domain.Customer;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;
using Intime.OPC.Domain.ReturnGoods;
using Intime.OPC.Infrastructure;
using Intime.OPC.Infrastructure.Mvvm.Utility;

namespace Intime.OPC.Modules.GoodsReturn.ViewModel
{
    [Export(typeof (ReturnPackagePrintExpressageConnectViewModel))]
    public class ReturnPackagePrintExpressageConnectViewModel : BindableBase
    {
        private List<Order> _orderList;
        private RMADto _rmaDto;
        private List<RMADto> _rmaDtoList;
        private RmaExpressDto _rmaExpressDto;
        private RmaExpressSaveDto _rmaExpressSave;
        private List<OPC_ShippingSale> _shipList;
        private OPC_ShippingSale _shipSale;

        public ReturnPackagePrintExpressageConnectViewModel()
        {
            RmaExpressDto = new RmaExpressDto();

            CommandSearch = new AsyncDelegateCommand(SearchShip);
            CommandPrintView = new DelegateCommand(PrintView);
            CommandOnlyPrint = new DelegateCommand(OnlyPrint);
            CommandPrintExpress = new DelegateCommand(PrintExpressComplete);
            CommandSetShippingRemark = new DelegateCommand(SetShippingRemark);
            CommandSetRmaRemark = new DelegateCommand(SetRmaRemark);
            CommandSetOrderRemark = new DelegateCommand(SetOrderRemark);
            CommandGetRmaOrOrderByShipNo = new AsyncDelegateCommand(GetRmaOrOrderByShipNo);
            InitCombo();
        }

        public OPC_ShippingSale ShipSaleSelected
        {
            get { return _shipSale; }
            set { SetProperty(ref _shipSale, value); }
        }

        public ShipVia ShipVia { get; set; }
        public List<ShipVia> ShipViaList { get; set; }

        public List<OPC_ShippingSale> ShipSaleList
        {
            get { return _shipList; }
            set { SetProperty(ref _shipList, value); }
        }

        public List<Order> OrderList
        {
            get { return _orderList; }
            set { SetProperty(ref _orderList, value); }
        }

        public ICommand CommandSetRmaRemark { get; set; }
        public ICommand CommandGetRmaOrOrderByShipNo { get; set; }
        public ICommand CommandSetShippingRemark { get; set; }

        //快递单备注
        public ICommand CommandSaveShip { get; set; }
        public ICommand CommandPrintExpress { get; set; }
        public ICommand CommandOnlyPrint { get; set; }
        public ICommand CommandPrintView { get; set; }
        public ICommand CommandSearch { get; set; }
        public ICommand CommandSearchRmaDtoInfo { get; set; }
        public ICommand CommandGetRmaDetailByRmaNo { get; set; }
        public ICommand CommandSetOrderRemark { get; set; }

        public RMADto RMADto
        {
            get { return _rmaDto; }
            set { SetProperty(ref _rmaDto, value); }
        }

        public List<RMADto> RMADtoList
        {
            get { return _rmaDtoList; }
            set { SetProperty(ref _rmaDtoList, value); }
        }

        public RmaExpressDto RmaExpressDto
        {
            get { return _rmaExpressDto; }
            set { SetProperty(ref _rmaExpressDto, value); }
        }

        public RmaExpressSaveDto RmaExpressSaveDto
        {
            get { return _rmaExpressSave; }
            set { SetProperty(ref _rmaExpressSave, value); }
        }

        private void SetOrderRemark()
        {
            string id = RMADto.RMANo;
            var remarkWin = AppEx.Container.GetInstance<IRemark>();
            remarkWin.ShowRemarkWin(id, EnumSetRemarkType.SetOrderRemark);
        }

        private void GetRmaOrOrderByShipNo()
        {
            if (ShipSaleSelected != null)
            {
                RMADtoList =
                    AppEx.Container.GetInstance<IPackageService>()
                        .GetRmaForPrintExpressConnect(ShipSaleSelected.RmaNo)
                        .ToList();
                OrderList =
                    AppEx.Container.GetInstance<IPackageService>()
                        .GetOrderForPrintExpressConnect(ShipSaleSelected.OrderNo)
                        .ToList();
            }
            ClearData();
        }

        public void SetRmaRemark()
        {
            string id = RMADto.RMANo;
            var remarkWin = AppEx.Container.GetInstance<IRemark>();
            remarkWin.ShowRemarkWin(id, EnumSetRemarkType.SetRMARemark);
        }


        private void ClearData()
        {
            RMADtoList.Clear();
            OrderList.Clear();
        }

        private void SearchShip()
        {
            try
            {
                ClearData();
                ShipSaleList =
                    AppEx.Container.GetInstance<IPackageService>().GetShipListWithReturnGoodsConnect(RmaExpressDto).ToList();
            }
            catch { };
        }

        private void SetShippingRemark()
        {
            //被选择的对象
            if (ShipSaleSelected == null)
            {
                MvvmUtility.ShowMessageAsync("请选择快递单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string id = ShipSaleSelected.ExpressCode;
            var remarkWin = AppEx.Container.GetInstance<IRemark>();
            remarkWin.ShowRemarkWin(id, EnumSetRemarkType.SetShipSaleRemark); //填写的是快递单
        }

        public async void PrintExpressComplete()
        {
            if (ShipSaleSelected == null)
            {
                await MvvmUtility.ShowMessageAsync("请选择快递单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(ShipSaleSelected.RmaNo))
            {
                await MvvmUtility.ShowMessageAsync("请先保存快递信息", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            bool flag = AppEx.Container.GetInstance<IPackageService>().ShipPrintComplateConnect(ShipSaleSelected.ExpressCode);
            await MvvmUtility.ShowMessageAsync(flag ? "操作成功" : "操作失败", "提示", MessageBoxButton.OK, flag ? MessageBoxImage.Information : MessageBoxImage.Error);
            if (flag)
            {
                ClearData();
            }
        }

        public void OnlyPrint()
        {

        }

        private bool PrintCommon(bool falg = false)
        {
            /*
            if (SaleList == null || SaleSelected == null)
            {
                MvvmUtility.ShowMessage("请勾选要打印预览的销售单", "提示");
                return false;
            }
            IPrint pr = new PrintWin();
            string xsdName = "InvoiceDataSet";
            string rdlcName = "Print//SalesOrder.rdlc";

            var invoiceModel = new PrintModel();

            var salelist = new List<OPC_Sale>();
            //SaleSelected.TransName = SaleSelected.IfTrans;
            salelist.Add(SaleSelected);
            invoiceModel.SaleDT = salelist;
            invoiceModel.SaleDetailDT = InvoiceDetail4List;
            pr.Print(xsdName, rdlcName, invoiceModel, falg);
            */
            return true;

        }

        public void PrintView()
        {

        }

        public void InitCombo()
        {
            ShipViaList = AppEx.Container.GetInstance<ICommonInfo>().GetShipViaList();
        }
    }
}