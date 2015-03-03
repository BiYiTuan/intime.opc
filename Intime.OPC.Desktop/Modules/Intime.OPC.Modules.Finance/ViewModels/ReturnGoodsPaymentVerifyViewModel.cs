using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Intime.OPC.Infrastructure.Mvvm;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Intime.OPC.DataService.Interface.Financial;
using Intime.OPC.DataService.Interface.RMA;
using Intime.OPC.DataService.Interface.Trans;
using Intime.OPC.DataService.IService;
using Intime.OPC.Domain.Customer;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Infrastructure;

namespace Intime.OPC.Modules.Finance.ViewModels
{
    [Export("ReturnPackageManageViewModel", typeof (ReturnGoodsPaymentVerifyViewModel))]
    public class ReturnGoodsPaymentVerifyViewModel : BindableBase
    {
        private List<RMADto> _rmaDtos;
        private List<SaleRmaDto> _saleRmaList;
        private ReturnGoodsPayDto returnGoodsDto;
        public decimal rmaDecimal;
        private List<RmaDetail> rmaDetails;
        public RMADto rmaDto;
        private SaleRmaDto saleRma;

        public ReturnGoodsPaymentVerifyViewModel()
        {
            ReturnGoodsPayDto = new ReturnGoodsPayDto();
            CommandSearch = new AsyncQueryCommand(SearchSaleRma, () => SaleRmaList, "收货单");
            CommandGetRmaSaleDetailByRma = new AsyncDelegateCommand(GetRmaSaleDetailByRma);
            CommandCustomerReturnGoodsSave = new AsyncDelegateCommand(CustomerReturnGoodsSave);
            CommandGetRmaByOrder = new AsyncDelegateCommand(GetRmaByOrder);
            CommandGetRmaDetailByRma = new AsyncDelegateCommand(GetRmaDetailByRma);
            CommandSetRmaRemark = new DelegateCommand(SetRmaRemark);
            InitCombo();
        }

        public ReturnGoodsPayDto ReturnGoodsPayDto
        {
            get { return returnGoodsDto; }
            set { SetProperty(ref returnGoodsDto, value); }
        }

        public List<SaleRmaDto> SaleRmaList
        {
            get { return _saleRmaList; }
            set { SetProperty(ref _saleRmaList, value); }
        }

        public IList<KeyValue> StoreList { get; set; }
        public IList<KeyValue> PaymentTypeList { get; set; }

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

        public ICommand CommandSearch { get; set; }
        public ICommand CommandGetRmaByOrder { get; set; }
        public ICommand CommandGetRmaSaleDetailByRma { get; set; }
        public ICommand CommandCustomerReturnGoodsSave { get; set; }
        public ICommand CommandGetRmaDetailByRma { get; set; }
        public ICommand CommandSetRmaRemark { get; set; }

        public decimal RmaDecimal
        {
            get { return rmaDecimal; }
            set { SetProperty(ref rmaDecimal, value); }
        }

        public SaleRmaDto SaleRma
        {
            get { return saleRma; }
            set { SetProperty(ref saleRma, value); }
        }

        public void SearchSaleRma()
        {
            if (RmaDetailList != null)
            {
                RmaDetailList.Clear();
            }
            if (RmaList != null)
            {
                RmaList.Clear();
            }
            SaleRmaList = AppEx.Container.GetInstance<IPaymentVerificationService>().GetRmaByReturnGoodPay(ReturnGoodsPayDto).ToList();
        }

        public void GetRmaDetailByRma()
        {
            if (RmaDto == null)
            {
                //MvvmUtility.ShowMessage("请选择退货单", "提示");
                return;
            }
            RmaDetailList = AppEx.Container.GetInstance<IPackageService>().GetRmaDetailByRma(RmaDto.RMANo).ToList();
        }

        public void GetRmaByOrder()
        {
            if (SaleRma == null)
            {
                RmaList.Clear();
                RmaDetailList.Clear();
                return;
            }

            RmaList = AppEx.Container.GetInstance<IPaymentVerificationService>().GetRmaByRmaOder(SaleRma.RmaNo).ToList();
			MvvmUtility.WarnIfEmpty(RmaList, "退货单");

            if (RmaList != null)
            {
                RmaDecimal = (decimal)RmaList.Sum(rma => rma.RMAAmount);
            }
        }

        public async void CustomerReturnGoodsSave()
        {
            if (RmaDecimal == 0)
            {
                await MvvmUtility.ShowMessageAsync("实退总金额必须大于0", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (SaleRma == null)
            {
                await MvvmUtility.ShowMessageAsync("请选择收货单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            bool flag = AppEx.Container.GetInstance<IPaymentVerificationService>()
                .ReturnGoodsPayVerify(SaleRma.RmaNo, rmaDecimal);
            await MvvmUtility.ShowMessageAsync(flag ? "退货付款确认成功" : "退货付款确认失败", "提示", MessageBoxButton.OK, flag ? MessageBoxImage.Information : MessageBoxImage.Error);
            if (flag)
            {
                SearchSaleRma();
            }
        }

        public void InitCombo()
        {
            StoreList = AppEx.Container.GetInstance<ICommonInfo>().GetStoreList();
            PaymentTypeList = AppEx.Container.GetInstance<ICommonInfo>().GetPayMethod();
        }

        public void GetRmaSaleDetailByRma()
        {
            if (rmaDto != null)
            {
                RmaDetailList = AppEx.Container.GetInstance<IPackageService>().GetRmaDetailByRma(rmaDto.RMANo).ToList();
                MvvmUtility.WarnIfEmpty(RmaDetailList, "退货单明细");
            }
        }

        public void SetRmaRemark()
        {
            string id = RmaDto.RMANo;
            var remarkWin = AppEx.Container.GetInstance<IRemark>();
            remarkWin.ShowRemarkWin(id, EnumSetRemarkType.SetRMARemark);
        }
    }
}