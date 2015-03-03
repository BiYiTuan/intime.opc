using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Intime.OPC.DataService.Interface.RMA;
using Intime.OPC.DataService.IService;
using Intime.OPC.Domain.Customer;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Infrastructure;
using System.Windows.Input;
using Intime.OPC.Infrastructure.Mvvm;
using Intime.OPC.Infrastructure.Mvvm.Utility;

namespace Intime.OPC.Modules.GoodsReturn.ViewModel
{
    [Export(typeof (ReturnPackageManageViewModel))]
    public class ReturnPackageManageViewModel : BindableBase
    {
        private List<RMADto> _rmaDtos;
        private RMADto _saleRma;
        private List<RMADto> _saleRmaList;
        private PackageReceiveDto packageReceiveDto;
        private List<RmaDetail> rmaDetails;

        public RMADto rmaDto;

        public ReturnPackageManageViewModel()
        {
            CommandSearch = new AsyncDelegateCommand(SearchRmaAndSaleRma);
            CommandGetRmaSaleDetailByRma = new AsyncDelegateCommand(GetRmaSaleDetailByRma);
            CommandGetRmaBySaleRma = new AsyncDelegateCommand(GetRmaBySaleRma);
            PackageReceiveDto = new PackageReceiveDto();
            CommandSetSaleRmaRemark = new DelegateCommand(SetSaleRmaRemark);
            CommandSetRmaRemark = new DelegateCommand(SetRmaRemark);
            CommandReceivingGoodsSubmit = new DelegateCommand(ReceivingGoodsSubmit);
        }

        public PackageReceiveDto PackageReceiveDto
        {
            get { return packageReceiveDto; }
            set { SetProperty(ref packageReceiveDto, value); }
        }

        public List<RMADto> SaleRmaList
        {
            get { return _saleRmaList; }
            set { SetProperty(ref _saleRmaList, value); }
        }

        public RMADto SaleRma
        {
            get { return _saleRma; }
            set { SetProperty(ref _saleRma, value); }
        }

        public RMADto RmaDto
        {
            get { return rmaDto; }
            set { SetProperty(ref rmaDto, value); }
        }

        public List<RMADto> RmaList
        {
            get { return _rmaDtos; }
            set { SetProperty(ref _rmaDtos, value); }
        }

        public List<RmaDetail> RmaDetailList
        {
            get { return rmaDetails; }
            set { SetProperty(ref rmaDetails, value); }
        }

        public ICommand CommandSetSaleRmaRemark { get; set; }
        public ICommand CommandReceivingGoodsSubmit { get; set; }
        public ICommand CommandSearch { get; set; }
        public ICommand CommandGetRmaSaleDetailByRma { get; set; }
        public ICommand CommandGetRmaBySaleRma { get; set; }
        public ICommand CommandSetRmaRemark { get; set; }

        public void SetRmaRemark()
        {
            string id = SaleRma.RMANo;
            var remarkWin = AppEx.Container.GetInstance<IRemark>();
            remarkWin.ShowRemarkWin(id, EnumSetRemarkType.SetRMARemark);
        }

        //收货单备注
        public void SetSaleRmaRemark()
        {
            string id = RmaDto.RMANo;
            var remarkWin = AppEx.Container.GetInstance<IRemark>();
            remarkWin.ShowRemarkWin(id, EnumSetRemarkType.SetSaleRMARemark);
        }

        public void GetRmaSaleDetailByRma()
        {
            if (rmaDto != null)
            {
                RmaDetailList = AppEx.Container.GetInstance<IPackageService>().GetRmaDetailByRma(rmaDto.RMANo).ToList();
            }
        }

        public async void ReceivingGoodsSubmit()
        {
            List<RMADto> saleRmaSelected = SaleRmaList.Where(e => e.IsSelected).ToList();
            if (saleRmaSelected.Count == 0)
            {
                await MvvmUtility.ShowMessageAsync("请勾选收货单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            bool flag =
                AppEx.Container.GetInstance<IPackageService>()
                    .ReceivingGoodsSubmit(saleRmaSelected.Select(e => e.RMANo).ToList());
            await MvvmUtility.ShowMessageAsync(flag ? "确认收货成功" : "确认收货失败", "提示", MessageBoxButton.OK, flag ? MessageBoxImage.Information : MessageBoxImage.Error);
            if (flag)
            {
                if (RmaDetailList!=null)
                    RmaDetailList.Clear();
                SearchRmaAndSaleRma();
            }
        }

        private void GetRmaBySaleRma()
        {
            if (SaleRma != null)
            {
                SaleRmaList = RmaList.Where(e => e.Id == SaleRma.Id).ToList();
            }
        }
        private void SearchRmaAndSaleRma()
        {
            RmaList = AppEx.Container.GetInstance<IPackageService>().GetRma(PackageReceiveDto).ToList();
        }
    }
}