using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Intime.OPC.DataService.Interface.Customer;
using Intime.OPC.DataService.Interface.Trans;
using Intime.OPC.DataService.IService;
using Intime.OPC.Domain.Customer;
using OPCAPP.Domain.Dto;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;
using Intime.OPC.Infrastructure.Mvvm.Utility;

namespace Intime.OPC.Modules.CustomerService.ViewModels
{
    [Export(typeof(CustomerSelfNetReturnGoodsViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CustomerSelfNetReturnGoodsViewModel : CustomerReturnGoodsViewModel
    {
        public CustomerSelfNetReturnGoodsViewModel()
        {
            CommandCustomerReturnGoodsPass = new DelegateCommand(SetCustomerReturnGoodsPass);
            CommandCustomerReturnGoodsSeftReject = new DelegateCommand(SetCustomerReturnGoodsSeftReject);
        }

        private async void SetCustomerReturnGoodsPass()
        {
            List<OrderItemDto> selectOrder = OrderItemList.Where(e => e.IsSelected).ToList();
            if (SaleRma == null)
            {
                await MvvmUtility.ShowMessageAsync("请选择订单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (selectOrder.Count == 0)
            {
                await MvvmUtility.ShowMessageAsync("请选择销售单明细", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            //原因必填
            if (string.Empty == RmaPost.Remark || RmaPost.Remark == null || RmaPost.Remark == "")
            {
                await MvvmUtility.ShowMessageAsync("退货备注不能为空", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            List<KeyValuePair<int, int>> list =
                selectOrder.Select(
                    e => new KeyValuePair<int, int>(e.Id, e.NeedReturnCount)).ToList<KeyValuePair<int, int>>();
            RmaPost.OrderNo = SaleRma.OrderNo;
            RmaPost.ReturnProducts = list;
            bool bFlag = AppEx.Container.GetInstance<ICustomerGoodsReturnService>().CustomerReturnGoodsSelfPass(RmaPost);
            await MvvmUtility.ShowMessageAsync(bFlag ? "退货审核成功" : "退货审核失败", "提示", MessageBoxButton.OK, bFlag ? MessageBoxImage.Information : MessageBoxImage.Error);
            if (bFlag)
            {
                ClearOrInitData();
                RmaPost = new RMAPost();
                ReturnGoodsSearch();
            }
        }

        public DelegateCommand CommandCustomerReturnGoodsSeftReject { get; set; }

        public DelegateCommand CommandCustomerReturnGoodsPass { get; set; }

        private async void SetCustomerReturnGoodsSeftReject()
        {
            List<OrderItemDto> selectOrder = OrderItemList.Where(e => e.IsSelected).ToList();
            if (SaleRma == null)
            {
                await MvvmUtility.ShowMessageAsync("请选择订单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (selectOrder.Count == 0)
            {
                await MvvmUtility.ShowMessageAsync("请选择销售单明细", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            List<KeyValuePair<int, int>> list =
                selectOrder.Select(
                    e => new KeyValuePair<int, int>(e.Id, e.NeedReturnCount)).ToList<KeyValuePair<int, int>>();
            RmaPost.OrderNo = SaleRma.OrderNo;
            RmaPost.ReturnProducts = list;
            bool bFlag = AppEx.Container.GetInstance<ICustomerGoodsReturnService>().CustomerReturnGoodsSelfReject(RmaPost);
            await MvvmUtility.ShowMessageAsync(bFlag ? "拒绝退货申请成功" : "拒绝退货申请失败", "提示", MessageBoxButton.OK, bFlag ? MessageBoxImage.Information : MessageBoxImage.Error);
            if (bFlag)
            {
                ClearOrInitData();
                RmaPost = new RMAPost();
                ReturnGoodsSearch();
            }
        }

        public override void GetOrderDetailByOrderNo()
        {
            if (SaleRma != null)
            {
                OrderItemList.Clear();
                IList<OrderItemDto> list = AppEx.Container.GetInstance<ICustomerGoodsReturnService>()
                    .GetOrderDetailByOrderNoWithSelf(SaleRma.OrderNo);
                OrderItemList = list.ToList();
            }

            MvvmUtility.WarnIfEmpty(OrderItemList, "订单明细");
        }

        public override void ReturnGoodsSearch()
        {
            SaleRmaList.Clear();
            IList<OPC_SaleRMA> list =
                AppEx.Container.GetInstance<ICustomerGoodsReturnService>().ReturnGoodsSearchForSelf(ReturnGoodsGet);
            if (list == null)
            {
                ClearOrInitData();
            }
            else
            {
                SaleRmaList = list.ToList();
            }
        }
    }
}

