using System;

namespace Intime.OPC.Domain.Partials.Models
{
    public class ShippingOrderModel
    {
        /// <summary>
        ///     销售单号
        /// </summary>
        public string SaleOrderNo { get; set; }

        /// <summary>
        ///     发货单ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     发货单号
        /// </summary>
        public string GoodsOutCode { get; set; }

        /// <summary>
        ///     快递单号
        /// </summary>
        public string ExpressCode { get; set; }

        /// <summary>
        ///     发货状态
        /// </summary>
        public int? ShippingStatus { get; set; }

        /// <summary>
        ///     收货人姓名
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        ///     收货人地址
        /// </summary>
        public string CustomerAddress { get; set; }

        /// <summary>
        ///     收货人电话
        /// </summary>
        public string CustomerPhone { get; set; }

        /// <summary>
        ///     发货时间
        /// </summary>
        public DateTime? GoodsOutDate { get; set; }

        /// <summary>
        ///     发货方式
        /// </summary>
        public string GoodsOutType { get; set; }

        /// <summary>
        ///     快递公司
        /// </summary>
        public string ShipCompanyName { get; set; }

        /// <summary>
        ///     快递员
        /// </summary>
        public string ShipManName { get; set; }

        /// <summary>
        /// 打印次数
        /// </summary>
        public int PrintTimes { get; set; }

        /// <summary>
        ///     邮编
        /// </summary>
        public string ShippingZipCode { get; set; }

        /// <summary>
        ///     配送方式
        /// </summary>
        /// <value>The shipping method.</value>
        public string ShippingMethod { get; set; }

        /// <summary>
        ///     支付快递公司快递费
        /// </summary>
        /// <value>The express fee.</value>
        public decimal ShipViaExpressFee { get; set; }

        /// <summary>
        ///     快递费
        /// </summary>
        /// <value>The express fee.</value>
        public decimal ExpressFee { get; set; }

        /// <summary>
        ///     退货单号
        /// </summary>
        /// <value>The shipping method.</value>
        public string RmaNo { get; set; }

        /// <summary>
        ///  订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// 快递公司ID
        /// </summary>
        public int? ShipViaId { get; set; }


        public string RMAAddress { get; set; }
        public string RMAZipCode { get; set; }
        public string RMAPerson { get; set; }
        public string RMAPhone { get; set; }
    }
}
