using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Intime.OPC.Domain.Dto
{
    /// <summary>
    /// 销售单
    /// </summary>
    public class SalesOrderDto
    {
        /// <summary>
        /// Id 主键ID
        /// </summary>

        public int Id { get; set; }

        /// <summary>
        /// 订单 NO
        /// </summary>

        public string OrderNo { get; set; }

        /// <summary>
        /// 销售单 NO
        /// </summary>

        public string SaleOrderNo { get; set; }

        /// <summary>
        ///     订单渠道号
        /// </summary>
        public string OrderChannelNo { get; set; }

        public int SalesType { get; set; }


        public int Status { get; set; }

        /// <summary>
        /// 状态名
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// 收银状态
        /// </summary>

        public int? CashStatus { get; set; }

        public string CashNum { get; set; }

        public DateTime CashDate { get; set; }

        /// <summary>
        /// 销售日期
        /// </summary>

        public DateTime SellDate { get; set; }

        /// <summary>
        /// 是否调拨
        /// </summary>

        public bool? IfTrans { get; set; }


        public string TransStatus { get; set; }

        /// <summary>
        /// 销售单金额
        /// </summary>

        public decimal SalesAmount { get; set; }

        /// <summary>
        /// 数量
        /// </summary>

        public int? SalesCount { get; set; }


        #region 订单相关

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

        #endregion

        #region 物流 相关信息

        /// <summary>
        /// 快递公司ID
        /// </summary>

        public int? ShipViaId { get; set; }

        /// <summary>
        /// 快递公司NAME
        /// </summary>

        public int? ShipViaName { get; set; }

        /// <summary>
        /// 物流单 code
        /// </summary>

        public string ShippingCode { get; set; }

        /// <summary>
        /// 物流 费
        /// </summary>

        public decimal ShippingFee { get; set; }

        /// <summary>
        /// 物流 状态
        /// </summary>

        public int? ShippingStatus { get; set; }

        /// <summary>
        /// 物流状态 Name
        /// </summary>

        public string ShippingStatusName { get; set; }

        /// <summary>
        /// 物流备注
        /// </summary>

        public string ShippingRemark { get; set; }

        #endregion

        #region stroe

        /// <summary>
        /// 门店ID
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string StoreName { get; set; }

        #endregion

        #region section

        /// <summary>
        /// 专柜ID
        /// </summary>
        [JsonProperty(PropertyName = "CounterId")]
        public int SectionId { get; set; }

        /// <summary>
        /// 专柜CODE
        /// </summary>
        [JsonProperty(PropertyName = "CounterCode")]
        public string SectionCode { get; set; }

        #endregion

        #region remark

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 备注
        /// 
        /// </summary>
        ////2009-02-15T00:00:00Z
        //[JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime RemarkDate { get; set; }

        #endregion


    }
}