
using System.Collections.Generic;
using Intime.OPC.Domain.Enums;

namespace Intime.OPC.Domain.BusinessModel
{
    public class SaleOrderFilter
    {
        /// <summary>
        /// 销售单 NO
        /// </summary>
        public string SalesOrderNo { get; set; }

        /// <summary>
        /// 订单NO
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 发货单 Id
        /// </summary>
        public int? ShippingOrderId { get; set; }

        /// <summary>
        /// 日期范围
        /// </summary>
        public DateRangeFilter DateRange { get; set; }

        /// <summary>
        /// 销售单状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 多状态iN 关系
        /// </summary>
        public List<int> Statuses { get; set; }

        /// <summary>
        /// 销售单状态
        /// </summary>
        public EnumSaleOrderStatus? SaleOrderStatus
        {
            get
            {
                return (Status == null || Status == -1) ? default(EnumSaleOrderStatus?) : (EnumSaleOrderStatus)Status;
            }
        }

        ///// <summary>
        ///// 是否生成发货单
        ///// </summary>
        //public bool? HasDeliveryOrderGenerated { get; set; }

        /// <summary>
        /// 查询指定门店
        /// </summary>
        public List<int> DataRoleStores { get; set; }

        /// <summary>
        /// 指定门店
        /// </summary>
        public int? StoreId { get; set; }

        ///// <summary>
        ///// 是否查询所有门店
        ///// </summary>
        //public bool IsAllStoreIds { get; set; }


        /// <summary>
        /// 订单类型 orderproducttype = 2 是迷你银
        /// </summary>
        public int? OrderProductType { get; set; }

        /// <summary>
        /// 销售单 收银状态 in
        /// </summary>
        public List<int> CashStatuses { get; set; }
    }
}
