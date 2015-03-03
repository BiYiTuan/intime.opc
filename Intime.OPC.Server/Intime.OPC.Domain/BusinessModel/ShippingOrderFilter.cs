using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain.Enums;

namespace Intime.OPC.Domain.BusinessModel
{
    public class ShippingOrderFilter
    {
        /// <summary>
        /// 销售单 no
        /// </summary>
        [System.Obsolete("请使用SalesOrderNo")]
        public string SaleOrderNo
        {
            get { return SalesOrderNo; }
            set { SalesOrderNo = value; }
        }

        /// <summary>
        /// 销售单号
        /// </summary>
        public string SalesOrderNo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 是否生成发货单
        /// </summary>
        public bool HasDeliveryOrderGenerated { get; set; }

        /// <summary>
        /// 查询指定门店
        /// </summary>
        public List<int> StoreIds { get; set; }

        /// <summary>
        /// 是否查询所有门店
        /// </summary>
        public bool IsAllStoreIds { get; set; }

        /// <summary>
        /// 日期范围 订单时间
        /// </summary>
        public DateRangeFilter DateRange { get; set; }

        /// <summary>
        /// 发货时间
        /// </summary>
        public DateTime? ShipStartDate { get; set; }

        /// <summary>
        /// 发货时间
        /// </summary>
        public DateTime? ShipEndDate { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        public string ExpressNo { get; set; }

        /// <summary>
        /// 顾客手机号
        /// </summary>
        public string CustomersPhoneNumber { get; set; }
    }
}
