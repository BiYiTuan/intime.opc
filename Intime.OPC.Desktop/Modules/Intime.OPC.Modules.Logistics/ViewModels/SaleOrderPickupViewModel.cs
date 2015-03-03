using System.ComponentModel.Composition;
using Intime.OPC.Domain.Enums;

namespace Intime.OPC.Modules.Logistics.ViewModels
{
    [Export("SaleOrderPickupViewModel")]
    public class SaleOrderPickupViewModel : StoreInViewModel
    {
        public SaleOrderPickupViewModel()
        {
            SearchSaleStatus = EnumSearchSaleStatus.ShipedSearchStatus;
        }

        public override string GetFilter()
        {
            return string.Format("startdate={0}&enddate={1}&orderno={2}&saleorderno={3}&pageIndex={4}&pageSize={5}",
                Invoice4Get.StartSellDate.ToShortDateString(),
                Invoice4Get.EndSellDate.ToShortDateString(),
                Invoice4Get.OrderNo, Invoice4Get.SaleOrderNo, 1, 50);
        }
    }
}