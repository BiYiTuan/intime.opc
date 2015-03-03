using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Intime.OPC.DataService.Customer;
using Intime.OPC.DataService.Interface.RMA;
using Intime.OPC.DataService.Interface.Trans;
using Intime.OPC.DataService.IService;
using Intime.OPC.Domain.Customer;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;
using Intime.OPC.Domain.ReturnGoods;
using Intime.OPC.Infrastructure;
using System.Windows.Input;
using Intime.OPC.Infrastructure.Mvvm;
using Intime.OPC.Infrastructure.Mvvm.Utility;

namespace Intime.OPC.Modules.GoodsReturn.ViewModel
{
    [Export(typeof (ReturnPackagePrintExpressViewModel))]
    public class ReturnPackagePrintExpressViewModel : BindableBase
    {
        private List<RmaDetail> _rmaDetails;
        private RMADto _rmaDto;
        private List<RMADto> _rmaDtoList;
        private RmaExpressDto _rmaExpressDto;
        private RmaExpressSaveDto _rmaExpressSave;
        private List<OPC_ShippingSale> _shipList;
        private OPC_ShippingSale _shipSale;

        public ReturnPackagePrintExpressViewModel()
        {
            RmaExpressDto = new RmaExpressDto();
            RmaExpressSaveDto = new RmaExpressSaveDto();
            CommandGetRmaDetailByRmaNo = new AsyncDelegateCommand(GetRmaDetailByRmaNo);
            CommandGetRmaByRmaNo = new AsyncDelegateCommand(GetRmaByRmaNo);
            CommandSearch = new AsyncDelegateCommand(SearchShip);
            CommandPrintView = new DelegateCommand(PrintView);
            CommandOnlyPrint = new DelegateCommand(OnlyPrint);
            CommandPrintExpress = new DelegateCommand(PrintExpressComplete);
            CommandSaveShip = new DelegateCommand(SaveShip);
            CommandSetShippingRemark = new DelegateCommand(SetShippingRemark);
            CommandSetRmaRemark = new DelegateCommand(SetRmaRemark);

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

        public ICommand CommandSetRmaRemark { get; set; }
        public ICommand CommandGetRmaByRmaNo { get; set; }
        public ICommand CommandSetShippingRemark { get; set; }

        //快递单备注
        public ICommand CommandSaveShip { get; set; }
        public ICommand CommandPrintExpress { get; set; }
        public ICommand CommandOnlyPrint { get; set; }
        public ICommand CommandPrintView { get; set; }
        public ICommand CommandSearch { get; set; }

        public RMADto RMADto
        {
            get { return _rmaDto; }
            set { SetProperty(ref _rmaDto, value); }
        }


        public List<RmaDetail> RmaDetailList
        {
            get { return _rmaDetails; }
            set { SetProperty(ref _rmaDetails, value); }
        }

        public List<RMADto> RMADtoList
        {
            get { return _rmaDtoList; }
            set { SetProperty(ref _rmaDtoList, value); }
        }


        public ICommand CommandSearchRmaDtoInfo { get; set; }
        public ICommand CommandGetRmaDetailByRmaNo { get; set; }

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

        public void SetRmaRemark()
        {
            string id = RMADto.RMANo;
            var remarkWin = AppEx.Container.GetInstance<IRemark>();
            remarkWin.ShowRemarkWin(id, EnumSetRemarkType.SetRMARemark);
        }

        private void GetRmaByRmaNo()
        {
            if (ShipSaleSelected != null)
            {
                ClearRmaData();
                RMADtoList =
                    AppEx.Container.GetInstance<IPackageService>()
                        .GetRmaForPrintExpress(ShipSaleSelected.RmaNo)
                        .ToList();
            }
        }

        private void ClearRmaData()
        {
            if (RmaDetailList != null)
            {
                RmaDetailList.Clear();
            }
            if (RMADtoList != null)
            {
                RMADtoList.Clear();
            }
        }

        private void SearchShip()
        {
            ClearRmaData();
            ShipSaleList = AppEx.Container.GetInstance<IPackageService>().GetShipListWithReturnGoods(RmaExpressDto).ToList();
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

        public async void SaveShip()
        {
            if (ShipSaleSelected == null)
            {
                await MvvmUtility.ShowMessageAsync("请选择快递单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                if (ShipVia == null) {
                    await MvvmUtility.ShowMessageAsync("请选择快递公司", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (RmaExpressSaveDto == null || string.IsNullOrEmpty(RmaExpressSaveDto.ShippingCode))
                {
                    await MvvmUtility.ShowMessageAsync("请录入快递单号", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (RmaExpressSaveDto == null || RmaExpressSaveDto.ShippingFee==0)
                {
                    await MvvmUtility.ShowMessageAsync("请录入运费", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                RmaExpressSaveDto.ShipViaID = ShipVia.Id;
                RmaExpressSaveDto.ShipViaName = ShipVia.Name;
                RmaExpressSaveDto.RmaNo = ShipSaleSelected.RmaNo;
                bool succeeded = AppEx.Container.GetInstance<IPackageService>().UpdateShipWithReturnExpress(RmaExpressSaveDto);
                if (succeeded) 
                {
                    SearchShip();
                }
            }
            catch
            {
            }
        }

        public void PrintExpressComplete() //
        {
            if (ShipSaleSelected == null)
            {
                MvvmUtility.ShowMessageAsync("请选择快递单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(ShipSaleSelected.ExpressCode))
            {
                MvvmUtility.ShowMessageAsync("请先保存快递信息", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            bool flag = AppEx.Container.GetInstance<IPackageService>().ShipPrintComplete(ShipSaleSelected.ExpressCode);
            MvvmUtility.ShowMessageAsync(flag ? "操作成功" : "操作失败", "提示", MessageBoxButton.OK, flag ? MessageBoxImage.Information : MessageBoxImage.Error);
            if (flag)
            {
                ClearRmaData();
            }
        }

        public void OnlyPrint()
        {
            if (ShipSaleSelected == null)
            {
                MvvmUtility.ShowMessageAsync("请选择快递单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(ShipSaleSelected.ExpressCode))
            {
                MvvmUtility.ShowMessageAsync("请先保存快递信息", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            bool flag = AppEx.Container.GetInstance<IPackageService>().ShipPrint(ShipSaleSelected.ExpressCode);
            MvvmUtility.ShowMessageAsync(flag ? "操作成功" : "操作失败", "提示", MessageBoxButton.OK, flag ? MessageBoxImage.Information : MessageBoxImage.Error);
        }


        public void PrintView()
        {

        }

        public void InitCombo()
        {
            ShipViaList = AppEx.Container.GetInstance<ICommonInfo>().GetShipViaList();
        }

        public void GetRmaDetailByRmaNo()
        {
            if (RMADto != null)
            {
                RmaDetailList = AppEx.Container.GetInstance<ICustomerGoodsReturnQueryService>()
                    .GetRmaDetailByRmaNo(RMADto.RMANo).ToList();
            }
        }
    }
}