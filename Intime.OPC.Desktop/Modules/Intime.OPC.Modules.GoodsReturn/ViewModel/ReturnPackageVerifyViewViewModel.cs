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

namespace Intime.OPC.Modules.GoodsReturn.ViewModels
{
    [Export(typeof (ReturnPackageVerifyViewViewModel))]
    public class ReturnPackageVerifyViewViewModel : BindableBase
    {
        private PackageReceiveDto _packageReceiveDto;
        private List<RMADto> _rmaDtos;
        private List<RmaDetail> rmaDetails;
        //与包裹审核公用传输类

        public RMADto rmaDto;

        public ReturnPackageVerifyViewViewModel()
        {
            CommandSearch = new AsyncDelegateCommand(SearchRma);
            PackageReceiveDto = new PackageReceiveDto();
            CommandGetRmaDetailByRma = new AsyncDelegateCommand(GetRmaDetailByRma);
            CommandTransVerifyPass = new DelegateCommand(TransVerifyPass);
            CommandTransVerifyNoPass = new DelegateCommand(TransVerifyNoPass);
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

        public List<RmaDetail> RmaDetailLs
        {
            get { return rmaDetails; }
            set { SetProperty(ref rmaDetails, value); }
        }

        public ICommand CommandSearch { get; set; }
        public ICommand CommandGetRmaDetailByRma { get; set; }
        public ICommand CommandTransVerifyPass { get; set; }
        public ICommand CommandTransVerifyNoPass { get; set; }
        public ICommand CommandSetRmaRemark { get; set; }

        public void SetRmaRemark()
        {
            string id = RmaDto.RMANo;
            var remarkWin = AppEx.Container.GetInstance<IRemark>();
            remarkWin.ShowRemarkWin(id, EnumSetRemarkType.SetRMARemark);
        }

        public void TransVerifyNoPass()
        {
            if (RmaList == null)
            {
                MvvmUtility.ShowMessageAsync("请选择退货单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            List<RMADto> rmaSelectedList = RmaList.Where(e => e.IsSelected).ToList();
            if (rmaSelectedList.Count == 0)
            {
                MvvmUtility.ShowMessageAsync("请选择退货单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            bool flag =
                AppEx.Container.GetInstance<IPackageService>()
                    .TransVerifyNoPass(rmaSelectedList.Select(e => e.RMANo).ToList());
            MvvmUtility.ShowMessageAsync(flag ? "设置审核不通过成功" : "设置审核不通过失败", "提示", MessageBoxButton.OK, flag ? MessageBoxImage.Information : MessageBoxImage.Error);
            if (flag)
            {
                if (RmaList != null)
                    RmaList.Clear();
                if (RmaDetailLs != null)
                    RmaDetailLs.Clear();
                SearchRma();
            }
        }

        public void TransVerifyPass()
        {
            if (RmaList == null)
            {
                MvvmUtility.ShowMessageAsync("请选择退货单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            List<RMADto> rmaSelectedList = RmaList.Where(e => e.IsSelected).ToList();
            if (rmaSelectedList.Count == 0)
            {
                MvvmUtility.ShowMessageAsync("请选择退货单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            bool flag =
                AppEx.Container.GetInstance<IPackageService>()
                    .TransVerifyPass(rmaSelectedList.Select(e => e.RMANo).ToList());
            MvvmUtility.ShowMessageAsync(flag ? "物流审核成功" : "物流审核失败", "提示", MessageBoxButton.OK, flag ? MessageBoxImage.Information : MessageBoxImage.Error);
            if (flag)
            {
                if (RmaList != null)
                    RmaList.Clear();
                if (RmaDetailLs != null)
                    RmaDetailLs.Clear();
                SearchRma();
            }

        }

        public void GetRmaDetailByRma()
        {
            if (rmaDto != null)
            {
                RmaDetailLs = AppEx.Container.GetInstance<IPackageService>().GetRmaDetailByRma(rmaDto.RMANo).ToList();
            }

            //if (RmaDto == null)
            //{
            //    MvvmUtility.ShowMessage("请选择退货单", "提示");
            //    return;
            //}
            //RmaDetailList = AppEx.Container.GetInstance<IPackageService>().GetRmaDetailByRma(RmaDto.RMANo).ToList();
        }

        public void SearchRma()
        {
            RmaList = AppEx.Container.GetInstance<IPackageService>().GetRmaByFilter(PackageReceiveDto).ToList();
        }
    }
}