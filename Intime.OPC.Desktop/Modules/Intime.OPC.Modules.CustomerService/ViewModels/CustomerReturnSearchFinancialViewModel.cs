using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Intime.OPC.DataService.Customer;
using Intime.OPC.Domain.Customer;
using Intime.OPC.Infrastructure;
using System.Windows.Forms;
using System;
using Intime.OPC.Infrastructure.Mvvm.Utility;

namespace Intime.OPC.Modules.CustomerService.ViewModels
{
    [Export(typeof (CustomerReturnSearchFinancialViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CustomerReturnSearchFinancialViewModel : CustomerReturnSearchViewModel
    {
        public CustomerReturnSearchFinancialViewModel()
        {
            //IsShowCustomerReViewBtn = false;
            IsShowCustomerAgreeBtn = false;
        }

        public override void SearchGoodsInfo()
        {
            OrderDtoList =
                    AppEx.Container.GetInstance<ICustomerGoodsReturnQueryService>()
                        .ReturnGoodsFinancialSearch(ReturnGoodsInfoGet)
                        .ToList();
            MvvmUtility.WarnIfEmpty(OrderDtoList, "订单");
        }

        public override void SearchRmaDtoListInfo()
        {
            if (OrderDto == null)
            {
                if (RmaDetailList != null) RmaDetailList.Clear();
                return;
            }

            RMADtoList = AppEx.Container.GetInstance<ICustomerGoodsReturnQueryService>().GetRmaFinancialByOrderNo(OrderDto.OrderNo).ToList();
            MvvmUtility.WarnIfEmpty(RMADtoList, "退货单");
        }
      
    }
}