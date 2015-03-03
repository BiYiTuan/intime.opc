using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Intime.OPC.Domain.Dto.Financial;
using Intime.OPC.Domain.Enums;
using Newtonsoft.Json;

namespace Intime.OPC.Domain.Dto.Request
{

    public class GetSaleOrderQueryRequest : DatePageRequest, IStoreDataRoleRequest
    {
        private string _salesorderNo;

        [System.Obsolete("建议使用SalesOrderNo")]
        public string SaleOrderNo
        {
            get { return _salesorderNo; }
            set { this._salesorderNo = value; }
        }

        /// <summary>
        /// 销售单号
        /// </summary>
        public string SalesOrderNo
        {
            get { return _salesorderNo; }
            set { this._salesorderNo = value; }
        }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 发货单号
        /// </summary>
        private int? _shippingorderId { get; set; }

        /// <summary>
        /// 发货单号 deliveryorderid
        /// </summary>
        public int? DeliveryOrderId
        {
            get { return ShippingOrderId; }
            set { ShippingOrderId = value; }
        }

        /// <summary>
        /// 发货单号
        /// </summary>
        public int? ShippingOrderId
        {
            get { return _shippingorderId; }
            set { _shippingorderId = value; }
        }


        public int? SortOrder { get; set; }

        /// <summary>
        /// 销售单状态
        /// </summary>
        [DataMember]
        public int? Status { get; set; }

        /// <summary>
        /// 多状态关系查询 
        /// </summary>
        public List<int> Statuses { get; set; }

        /// <summary>
        /// 查询指定门店
        /// </summary>
        public List<int> DataRoleStores { get; set; }

        /// <summary>
        /// 系统设置
        /// </summary>
        public int? CurrentUserId { get; set; }

        /// <summary>
        /// 指定门店
        /// </summary>
        public int? StoreId { get; set; }

        /// <summary>
        /// 订单类型 orderproducttype = 2 是迷你银
        /// </summary>
        public int? OrderProductType { get; set; }

        /// <summary>
        /// 销售单 收银状态 in
        /// </summary>
        public List<int> CashStatuses { get; set; }


        public override void ArrangeParams()
        {
            OrderProductType = CheckIsNullOrAndSet(OrderProductType);
            StoreId = CheckIsNullOrAndSet(StoreId);
            DeliveryOrderId = CheckIsNullOrAndSet(DeliveryOrderId);
            Status = CheckIsNullOrAndSet(Status);
            base.ArrangeParams();
        }
    }

    //[DataContract]
    //public class SaleOrderQueryRequest : DatePageRequest
    //{
    //    [DataMember(Name = "saleorderno")]
    //    public string SaleOrderNo { get; set; }

    //    [DataMember(Name = "orderno")]
    //    public string OrderNo { get; set; }

    //    /// <summary>
    //    /// 发货单号
    //    /// </summary>
    //    [JsonProperty(PropertyName = "DeliveryOrderId")]
    //    [DataMember(Name = "DeliveryOrderId")]
    //    public int? ShippingOrderId { get; set; }

    //    [DataMember]
    //    public int? SortOrder { get; set; }


    //    /// <summary>
    //    /// 销售单状态
    //    /// </summary>
    //    [DataMember]
    //    public int? Status { get; set; }

    //    /// <summary>
    //    /// 销售单状态
    //    /// </summary>
    //    public EnumSaleOrderStatus? SaleOrderStatus
    //    {
    //        get
    //        {
    //            return (Status == null || Status == -1) ? default(EnumSaleOrderStatus?) : (EnumSaleOrderStatus)Status;
    //        }
    //    }

    //    ///// <summary>
    //    ///// 是否生成发货单
    //    ///// </summary>
    //    //[DataMember]
    //    //public bool? HasDeliveryOrderGenerated { get; set; }

    //    /// <summary>
    //    /// 查询指定门店
    //    /// </summary>
    //    public List<int> StoreIds { get; set; }

    //    /// <summary>
    //    /// 是否查询所有门店
    //    /// </summary>
    //    public bool IsAllStoreIds { get; set; }

    //    public override void ArrangeParams()
    //    {
    //        ShippingOrderId = CheckIsNullOrAndSet(ShippingOrderId);
    //        Status = CheckIsNullOrAndSet(Status);
    //        base.ArrangeParams();
    //    }
    //}

    //[DataContract]
    //public class DateRangeRequest
    //{
    //    private DateTime? _beginDate;
    //    private DateTime? _endDate;

    //    [DataMember(Name = "startdate")]
    //    public DateTime? StartDate
    //    {
    //        get { return _beginDate; }
    //        set { _beginDate = value; }
    //    }

    //    [DataMember(Name = "enddate")]
    //    public DateTime? EndDate { get { return _endDate == null ? _endDate : _endDate.Value.Date.AddDays(1); } set { _endDate = value; } }
    //}
}
