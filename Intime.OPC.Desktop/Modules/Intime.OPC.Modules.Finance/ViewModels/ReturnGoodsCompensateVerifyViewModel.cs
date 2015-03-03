using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Intime.OPC.DataService.Interface.Financial;
using Intime.OPC.DataService.Interface.RMA;
using Intime.OPC.DataService.IService;
using Intime.OPC.Domain.Customer;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Infrastructure;
using System.Windows.Input;
using Intime.OPC.Infrastructure.Mvvm;
using Intime.OPC.Infrastructure.Mvvm.Utility;

namespace Intime.OPC.Modules.Finance.ViewModels
{
    [Export("ReturnGoodsCompensateVerifyViewModel", typeof (ReturnGoodsCompensateVerifyViewModel))]
    public class ReturnGoodsCompensateVerifyViewModel : BindableBase
    {
        private PackageReceiveDto _packageReceiveDto;
        private List<RMADto> _rmaDtos;
        private List<RmaDetail> rmaDetails;
        //与包裹审核公用传输类

        public RMADto rmaDto;

        public ReturnGoodsCompensateVerifyViewModel()
        {
            CommandSearch = new AsyncQueryCommand(SearchRma, () => RmaList, "退货单");
            PackageReceiveDto = new PackageReceiveDto();
            CommandGetRmaDetailByRma = new AsyncDelegateCommand(GetRmaDetailByRma);
            CommandFinancialVerifyPass = new DelegateCommand(FinancialVerifyPass);
            CommandFinancialVerifyNoPass = new DelegateCommand(FinancialVerifyNoPass);
            CommandSetRmaRemark = new DelegateCommand(SetRmaRemark);
        }

        public PackageReceiveDto PackageReceiveDto
        {
            get { return _packageReceiveDto; }
            set { SetProperty(ref _packageReceiveDto, value); }
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

        public ICommand CommandSearch { get; set; }
        public ICommand CommandGetRmaDetailByRma { get; set; }
        public ICommand CommandFinancialVerifyPass { get; set; }
        public ICommand CommandFinancialVerifyNoPass { get; set; }
        public ICommand CommandSetRmaRemark { get; set; }

        public void SetRmaRemark()
        {
            string id = RmaDto.RMANo;
            var remarkWin = AppEx.Container.GetInstance<IRemark>();
            remarkWin.ShowRemarkWin(id, EnumSetRemarkType.SetRMARemark);
        }

        public async void FinancialVerifyNoPass()
        {
            if (RmaList == null)
            {
                await MvvmUtility.ShowMessageAsync("请选择退货单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            List<RMADto> rmaSelectedList = RmaList.Where(e => e.IsSelected).ToList();
            if (rmaSelectedList.Count == 0)
            {
                await MvvmUtility.ShowMessageAsync("请选择退货单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            bool flag =
                AppEx.Container.GetInstance<IPaymentVerificationService>()
                    .FinancialVerifyNoPass(rmaSelectedList.Select(e => e.RMANo).ToList());
            await MvvmUtility.ShowMessageAsync(flag ? "设置审核未通过成功" : "设置审核未通过失败", "提示", MessageBoxButton.OK, flag ? MessageBoxImage.Information : MessageBoxImage.Error);
            if (flag)
            {
                SearchRma();
            }
        }

        public async void FinancialVerifyPass()
        {
            if (RmaList == null)
            {
                await MvvmUtility.ShowMessageAsync("请选择退货单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            List<RMADto> rmaSelectedList = RmaList.Where(e => e.IsSelected).ToList();
            if (rmaSelectedList.Count == 0)
            {
                await MvvmUtility.ShowMessageAsync("请选择退货单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            bool flag =
                AppEx.Container.GetInstance<IPaymentVerificationService>()
                    .FinancialVerifyPass(rmaSelectedList.Select(e => e.RMANo).ToList());
            await MvvmUtility.ShowMessageAsync(flag ? "设置审核通过成功" : "设置审核通过失败", "提示", MessageBoxButton.OK, flag ? MessageBoxImage.Information : MessageBoxImage.Error);
            if (flag)
            {
                SearchRma();
            }
        }

        public async void GetRmaDetailByRma()
        {
            if (RmaDto == null)
            {
                await MvvmUtility.ShowMessageAsync("请选择退货单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            RmaDetailList = AppEx.Container.GetInstance<IPackageService>().GetRmaDetailByRma(RmaDto.RMANo).ToList();

            MvvmUtility.WarnIfEmpty(RmaDetailList, "退货单明细");
        }

        public void SearchRma()
        {
            if (RmaDetailList != null)
            {
                RmaDetailList.Clear();
            }

            RmaList = AppEx.Container.GetInstance<IPaymentVerificationService>().GetRmaByFilter(PackageReceiveDto).ToList();
        }
    }
}