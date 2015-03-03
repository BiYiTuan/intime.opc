using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Intime.OPC.Infrastructure.Mvvm;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Intime.OPC.DataService.Customer;
using Intime.OPC.DataService.Interface.Customer;
using Intime.OPC.DataService.Interface.Trans;
using Intime.OPC.Domain.Customer;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Infrastructure;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace Intime.OPC.Modules.CustomerService.ViewModels
{
    public class CustomerReturnSearchViewModel : BindableBase
    {
        private bool _isShowCustomerAgree;
        private bool _isMultiSelectionEnalbed = true;
        private bool _isShowCustomerReViewBtn;
        private OrderDto _orderDto;
        private List<OrderDto> _orderDtoList;
        private ReturnGoodsInfoGet _returnGoodsInfo;
        private List<RmaDetail> _rmaDetails;
        private RMADto _rmaDto;
        private List<RMADto> _rmaDtoList;

        public CustomerReturnSearchViewModel()
        {
            ReturnGoodsInfoGet = new ReturnGoodsInfoGet();
            CommandSearchGoodsInfo = new AsyncDelegateCommand(SearchGoodsInfo);
            CommandSearchRmaDtoInfo = new AsyncQueryCommand(SearchRmaDtoListInfo, () => RMADtoList, "退货单");
            CommandAgreeReturnGoods = new DelegateCommand(SetAgreeReturnGoods);
            CommandGetRmaDetailByRmaNo = new AsyncDelegateCommand(GetRmaDetailByRmaNo);
            CommandSetCustomerMoneyReviewGoods = new DelegateCommand(SetCustomerMoneyGoods);
            ViewImageCommand = new DelegateCommand<int?>(OnImageView);
            ViewProductImageRequest = new InteractionRequest<ProductImageViewModel>();

            IsShowCustomerAgreeBtn = true;
            _isShowCustomerReViewBtn = true;
            InitCombo();
        }

        public InteractionRequest<ProductImageViewModel> ViewProductImageRequest { get; set; }

        public bool IsMultiSelectionEnalbed
        {
            get { return _isMultiSelectionEnalbed; }
            set { SetProperty(ref _isMultiSelectionEnalbed, value); }
        }

        public bool IsShowCustomerAgreeBtn
        {
            get { return _isShowCustomerAgree; }
            set { SetProperty(ref _isShowCustomerAgree, value); }
        }

        public bool IsShowCustomerReViewBtn
        {
            get { return _isShowCustomerReViewBtn; }
            set { SetProperty(ref _isShowCustomerReViewBtn, value); }
        }

        public IList<KeyValue> StoreList { get; set; }
        public IList<KeyValue> GetReturnDocStatusList { get; set; }
        public IList<KeyValue> PaymentTypeList { get; set; }

        public RMADto RMADto
        {
            get { return _rmaDto; }
            set { SetProperty(ref _rmaDto, value); }
        }

        public OrderDto OrderDto
        {
            get { return _orderDto; }
            set { SetProperty(ref _orderDto, value); }
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

        public List<OrderDto> OrderDtoList
        {
            get { return _orderDtoList; }
            set { SetProperty(ref _orderDtoList, value); }
        }

        public ICommand CommandSetCustomerMoneyReviewGoods { get; set; }
        public ICommand CommandAgreeReturnGoods { get; set; }
        public ICommand CommandSearchGoodsInfo { get; set; }
        public ICommand CommandSearchRmaDtoInfo { get; set; }
        public ICommand CommandGetRmaDetailByRmaNo { get; set; }
        public ICommand ViewImageCommand { get; set; }

        public ReturnGoodsInfoGet ReturnGoodsInfoGet
        {
            get { return _returnGoodsInfo; }
            set { SetProperty(ref _returnGoodsInfo, value); }
        }

        /// <summary>
        /// 查看商品图片
        /// </summary>
        private void OnImageView(int? rmaDetailId)
        {
            var rmaDetail = RmaDetailList.First(salesOrderDetail => salesOrderDetail.Id == rmaDetailId.Value);
#if DEBUG
            rmaDetail.ProductImageUrl = "http://irss.intime.com.cn/fileupload/img/product/20140707/0bc94489-254b-426b-b285-629ab69b2737_1000x0.jpg";
#endif
            var viewModel = new ProductImageViewModel
            {
                Title = "商品图片",
                ImageUrl = rmaDetail.ProductImageUrl
            };
            ViewProductImageRequest.Raise(viewModel);
        }

        private async void SetAgreeReturnGoods()
        {
            if (RMADtoList == null)
            {
                await MvvmUtility.ShowMessageAsync("请选择退货单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            List<RMADto> rmaSelectedList = RMADtoList.Where(e => e.IsSelected).ToList();
            bool flag = AppEx.Container.GetInstance<ICustomerGoodsReturnQueryService>().AgreeReturnGoods(rmaSelectedList.Select(e => e.RMANo).ToList());
            await MvvmUtility.ShowMessageAsync(flag ? "客服同意退货成功" : "客服同意退货失败", "提示", MessageBoxButton.OK, flag ? MessageBoxImage.Information : MessageBoxImage.Error);
            if (flag)
            {
                SearchRmaDtoListInfo();
            }
        }

        public void InitCombo()
        {
            // OderStatusList=new 
            StoreList = AppEx.Container.GetInstance<ICommonInfo>().GetStoreList();
            PaymentTypeList = AppEx.Container.GetInstance<ICommonInfo>().GetPayMethod();
            GetReturnDocStatusList = AppEx.Container.GetInstance<ICommonInfo>().GetReturnDocStatusList();
        }

        public void GetRmaDetailByRmaNo()
        {
            if (RMADto != null)
            {
                RmaDetailList = AppEx.Container.GetInstance<ICustomerGoodsReturnQueryService>()
                    .GetRmaDetailByRmaNo(RMADto.RMANo).ToList();
            }
        }

        public virtual async void SetCustomerMoneyGoods()
        {
            if (RMADtoList == null)
            {
                await MvvmUtility.ShowMessageAsync("请选择退货单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            List<RMADto> rmaSelectedList = RMADtoList.Where(e => e.IsSelected).ToList();
            if (rmaSelectedList.Count == 0)
            {
                await MvvmUtility.ShowMessageAsync("请选择退货单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            bool flag = false;
            try
            {
                flag =
                    AppEx.Container.GetInstance<ICustomerInquiryService>()
                        .SetCustomerMoneyGoods(rmaSelectedList.Select(e => e.RMANo).ToList());
            }
            catch (Exception Ex)
            {
                flag = false;
            }
            await MvvmUtility.ShowMessageAsync(flag ? "设置赔偿金额复审成功" : "设置赔偿金额复审失败", "提示", MessageBoxButton.OK, flag ? MessageBoxImage.Information : MessageBoxImage.Error);
          
            if (flag)
            {
                SearchRmaDtoListInfo();
            }
        }

        public virtual void SearchGoodsInfo()
        {
            OrderDtoList = AppEx.Container.GetInstance<ICustomerGoodsReturnQueryService>().ReturnGoodsRmaSearch(ReturnGoodsInfoGet).ToList();
            RMADtoList = null;
            RmaDetailList = null;

            MvvmUtility.WarnIfEmpty(OrderDtoList, "订单");
        }

        public virtual void SearchRmaDtoListInfo()
        {
            if (RmaDetailList != null) RmaDetailList.Clear();
            if (OrderDto == null)
            {
                if (RmaDetailList != null) RmaDetailList.Clear();
                return;
            }
            RMADtoList = AppEx.Container.GetInstance<ICustomerGoodsReturnQueryService>().GetRmaByOrderNo(OrderDto.OrderNo).ToList();
            if (RMADtoList == null || RMADtoList.Count == 0)
            {
                MvvmUtility.WarnIfEmpty(RMADtoList, "退货单");
                return;
            }

            RmaDetailList = AppEx.Container.GetInstance<ICustomerGoodsReturnQueryService>().GetRmaDetailByRmaNo(RMADtoList.First().RMANo).ToList();
        }
    }
}