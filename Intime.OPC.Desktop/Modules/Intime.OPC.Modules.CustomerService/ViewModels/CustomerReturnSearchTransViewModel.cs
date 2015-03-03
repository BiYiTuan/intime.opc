using System.ComponentModel.Composition;
using System.Linq;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using Intime.OPC.DataService.Customer;
using Intime.OPC.Domain.Customer;
using Intime.OPC.Infrastructure;

namespace Intime.OPC.Modules.CustomerService.ViewModels
{
    [Export(typeof (CustomerReturnSearchTransViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CustomerReturnSearchTransViewModel : CustomerReturnSearchViewModel
    {
        public CustomerReturnSearchTransViewModel()
        {
            IsShowCustomerReViewBtn = false;
        }
        public override void SearchGoodsInfo()
        {
            OrderDtoList = AppEx.Container.GetInstance<ICustomerGoodsReturnQueryService>().ReturnGoodsTransSearch(ReturnGoodsInfoGet).ToList();
            MvvmUtility.WarnIfEmpty(OrderDtoList, "订单");
        }

        public override void SearchRmaDtoListInfo()
        {
            if (OrderDto == null)
            {
                if (RmaDetailList != null) RmaDetailList.Clear();
                return;
            }

            RMADtoList = AppEx.Container.GetInstance<ICustomerGoodsReturnQueryService>().GetRmaTransByOrderNo(OrderDto.OrderNo).ToList();
            MvvmUtility.WarnIfEmpty(RMADtoList, "退货单");
        }
    }
}