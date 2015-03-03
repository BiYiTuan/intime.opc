using System;

namespace Intime.OPC.Domain.Dto
{
    public class OrderDto
    {

        //todo 缺少 门店应付款  备注时间
        /// <summary>
        ///     订单ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        ///     订单渠道号
        /// </summary>
        public string OrderChannelNo { get; set; }

        /// <summary>
        ///     支付方式
        /// </summary>
        public string PaymentMethodName { get; set; }

        /// <summary>
        ///     订单来源
        /// </summary>
        public string OrderSouce { get; set; }

        /// <summary>
        ///     订单状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        ///     商品数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        ///     商品金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        ///     顾客运费
        /// </summary>
        public decimal CustomerFreight { get; set; }

        /// <summary>
        ///     应付款合计
        /// </summary>
        public decimal MustPayTotal { get; set; }

        /// <summary>
        ///     购买时间
        /// </summary>
        public DateTime BuyDate { get; set; }

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
        ///     顾客备注
        /// </summary>
        public string CustomerRemark { get; set; }

        /// <summary>
        ///     是否要发票
        /// </summary>
        public string IfReceipt { get; set; }

        /// <summary>
        ///     发票台头
        /// </summary>
        public string ReceiptHead { get; set; }

        /// <summary>
        ///     发票内容
        /// </summary>
        public string ReceiptContent { get; set; }

        /// <summary>
        ///     发货方式
        /// </summary>
        public string OutGoodsType { get; set; }

        /// <summary>
        ///     邮编
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        ///     发货单号
        /// </summary>
        public string ShippingNo { get; set; }

        /// <summary>
        ///     快递单号
        /// </summary>
        public string ExpressNo { get; set; }

        /// <summary>
        ///     快递公司
        /// </summary>
        public string ExpressCompany { get; set; }

        /// <summary>
        ///     发货时间
        /// </summary>
        public DateTime OutGoodsDate { get; set; }
    }
}