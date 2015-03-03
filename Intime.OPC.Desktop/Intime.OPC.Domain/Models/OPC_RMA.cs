using System;

namespace Intime.OPC.Domain.Models
{
    public class OPC_RMA
    {
        /// <summary>
        ///     退货ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     退货单号
        /// </summary>
        public string RMANo { get; set; }

        /// <summary>
        ///     订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        ///     销售单号
        /// </summary>
        public string SaleOrderNo { get; set; }

        /// <summary>
        ///     门店
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        ///     退货原因
        /// </summary>
        public string RMAReason { get; set; }

        /// <summary>
        ///     退货状态
        /// </summary>
        public string RMAStatus { get; set; }

        /// <summary>
        ///     退货单状态
        /// </summary>
        public string RMABillStatus { get; set; }

        /// <summary>
        ///     退货单收银状态
        /// </summary>
        public string RMACashStatus { get; set; }

        /// <summary>
        ///     需求退货时间
        /// </summary>
        public DateTime RMAMustBackDate { get; set; }

        /// <summary>
        ///     退货金额
        /// </summary>
        public decimal RMAAmount { get; set; }

        /// <summary>
        ///     退货件数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        ///     收银流水号
        /// </summary>
        public string CashNumber { get; set; }

        /// <summary>
        ///     收银时间
        /// </summary>
        public DateTime CashDate { get; set; }

        /// <summary>
        ///     退货收银流水号
        /// </summary>
        public string RMACashNumber { get; set; }

        /// <summary>
        ///     退货收银时间
        /// </summary>
        public DateTime RMACashDate { get; set; }

        /// <summary>
        ///     退货类型
        /// </summary>
        public string RMAType { get; set; }

        /// <summary>
        ///     专柜码
        /// </summary>
        public string SectionId { get; set; }

        /// <summary>
        ///     退货时间
        /// </summary>
        public DateTime RMADate { get; set; }

        /// <summary>
        ///     支付方式
        /// </summary>
        public string CashType { get; set; }
    }
}