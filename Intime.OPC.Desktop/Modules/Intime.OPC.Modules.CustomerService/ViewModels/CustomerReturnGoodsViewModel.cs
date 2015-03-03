using System.Collections.Generic;
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
using Intime.OPC.Infrastructure;

namespace Intime.OPC.Modules.CustomerService.ViewModels
{
    [Export("CustomerReturnGoodsViewModel", typeof(CustomerReturnGoodsViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CustomerReturnGoodsViewModel : BindableBase
    {
        private OPC_SaleRMA _opcSaleRma;
        private OrderItemDto _orderItem;
        private List<OPC_SaleRMA> _orderList;
        private List<OrderItemDto> _orderitemList;
        private List<int> _returnList;
        private RMAPost rmaPost;

        public CustomerReturnGoodsViewModel()
        {
            CommandCustomerReturnGoodsSave = new DelegateCommand(CustomerReturnGoodsSave);
            CommandReturnGoodsSearch = new AsyncQueryCommand(ReturnGoodsSearch, () => SaleRmaList, "订单");
            CommandGetDown = new AsyncDelegateCommand(GetOrderDetailByOrderNo);
            CommandSetOrderRemark = new DelegateCommand(SetOrderRemark);
            ReturnGoodsGet = new ReturnGoodsGet();
            ClearOrInitData();
            InitCombo();
            RmaPost = new RMAPost();
        }

        public ICommand CommandComboSelect { get; set; }
        public ICommand CommandSetOrderRemark { get; set; }
        public ICommand CommandReturnGoodsSearch { get; set; }
        public ICommand CommandCustomerReturnGoodsSave { get; set; }
        public ICommand CommandGetDown { get; set; }

        //public IList<KeyValue> BrandList { get; set; }

        public IList<KeyValue> PaymentTypeList { get; set; }

        public RMAPost RmaPost
        {
            get { return rmaPost; }
            set { SetProperty(ref rmaPost, value); }
        }

        public List<OPC_SaleRMA> SaleRmaList
        {
            get { return _orderList; }
            set { SetProperty(ref _orderList, value); }
        }

        public OPC_SaleRMA SaleRma
        {
            get { return _opcSaleRma; }
            set { SetProperty(ref _opcSaleRma, value); }
        }

        public List<OrderItemDto> OrderItemList
        {
            get { return _orderitemList; }
            set { SetProperty(ref _orderitemList, value); }
        }

        public ReturnGoodsGet ReturnGoodsGet { get; set; }

        public List<int> ReturnCountList
        {
            get { return _returnList; }
            set { SetProperty(ref _returnList, value); }
        }

        public OrderItemDto OrderItem
        {
            get { return _orderItem; }
            set { SetProperty(ref _orderItem, value); }
        }

        public void InitCombo()
        {
            // OderStatusList=new 
            //BrandList = AppEx.Container.GetInstance<ICommonInfo>().GetBrandList();

            PaymentTypeList = AppEx.Container.GetInstance<ICommonInfo>().GetPayMethod();
        }

        public void SetOrderRemark()
        {
            //被选择的对象
            string id = SaleRma.OrderNo;
            var remarkWin = AppEx.Container.GetInstance<IRemark>();
            remarkWin.ShowRemarkWin(id, EnumSetRemarkType.SetSaleRMARemark); //4填写的是退货单
        }

        public void ClearOrInitData()
        {
            OrderItemList = new List<OrderItemDto>();
            SaleRmaList = new List<OPC_SaleRMA>();
        }

        /*点击销售单显示明细*/

        public virtual  void GetOrderDetailByOrderNo()
        {
            if (SaleRma != null)
            {
                OrderItemList.Clear();
                IList<OrderItemDto> list = AppEx.Container.GetInstance<ICustomerGoodsReturnService>()
                    .GetOrderDetailByOrderNo(SaleRma.OrderNo);
                OrderItemList = list.ToList();
            }

            MvvmUtility.WarnIfEmpty(OrderItemList, "订单明细");
        }

        /*退货单查询*/
        public  virtual  void ReturnGoodsSearch()
        {
            SaleRmaList.Clear();
            if (OrderItemList != null)
                OrderItemList.Clear();

            IList<OPC_SaleRMA> list =
                AppEx.Container.GetInstance<ICustomerGoodsReturnService>().ReturnGoodsSearch(ReturnGoodsGet);
            if (list == null)
            {
                ClearOrInitData();
            }
            else
            {
                SaleRmaList = list.ToList();
            }
        }

        /*客服退货*/

        private async void CustomerReturnGoodsSave()
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
            if (string.Empty == RmaPost.Remark || RmaPost.Remark==null || RmaPost.Remark=="")
            {
                await MvvmUtility.ShowMessageAsync("退货备注不能为空", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            List<KeyValuePair<int, int>> list =
                selectOrder.Select(
                    e => new KeyValuePair<int, int>(e.Id, e.NeedReturnCount)).ToList<KeyValuePair<int, int>>();
            //退货数量大于0，且已退数量+退货数量<=销售数量
            List<OrderItemDto> q= selectOrder.Where(t => (t.ReturnCount + t.NeedReturnCount > t.Quantity) || t.NeedReturnCount < 1).ToList();
            if (q.Count>0)
            {
                await MvvmUtility.ShowMessageAsync("选择的订单明细的退货数量必须大于零，且已退货数量加上退货数量要小于等于销售数量", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return ;
            }
            RmaPost.OrderNo = SaleRma.OrderNo;
            RmaPost.ReturnProducts = list;
            bool bFlag = AppEx.Container.GetInstance<ICustomerGoodsReturnService>().CustomerReturnGoodsSave(RmaPost);
            await MvvmUtility.ShowMessageAsync(bFlag ? "客服退货成功" : "客服退货失败", "提示", MessageBoxButton.OK, bFlag ? MessageBoxImage.Information : MessageBoxImage.Error);
            if (bFlag)
            {
                ClearOrInitData();
                RmaPost = new RMAPost();
                ReturnGoodsSearch();
            }
        }
    }
}