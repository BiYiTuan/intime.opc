using System;
using System.Collections.Generic;
using Intime.OPC.Domain.Enums;

namespace Intime.OPC.Domain.Dto.Request
{
    /// <summary>
    /// RMA qingqiu
    /// </summary>
    public class RmaQueryRequest : DatePageRequest, IStoreDataRoleRequest
    {
        public string OrderNo { get; set; }

        public int? Status { get; set; }

        public string RMANo { get; set; }


        private string _salesOrderNo;

        /// <summary>
        /// 销售单号
        /// </summary>
        [System.Obsolete("建议使用SalesOrderNo")]
        public string SaleOrderNo {
            get { return _salesOrderNo; }
            set { _salesOrderNo = value; }
        }

        /// <summary>
        /// 销售单号
        /// </summary>
        public string SalesOrderNo
        {
            get { return _salesOrderNo; }
            set { _salesOrderNo = value; }
        }

        public int? RmaStatus { get; set; }

        /// <summary>
        /// 多状态 IN 关系
        /// </summary>
        public List<int> Statuses { get; set; }

        public int? StoreId { get; set; }

        public int? SectionId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public int? CurrentUserId { get; set; }

        /// <summary>
        /// 数据权限
        /// </summary>
        public List<int> DataRoleStores { get; set; }

        /// <summary>
        /// 订单类型 orderproducttype = 2 是迷你银
        /// </summary>
        public int? OrderProductType { get; set; }

        public override void ArrangeParams()
        {
            Status = CheckIsNullOrAndSet(Status);
            StoreId = CheckIsNullOrAndSet(StoreId);
            SectionId = CheckIsNullOrAndSet(SectionId);
            OrderProductType = CheckIsNullOrAndSet(OrderProductType);

            //时间
            if (BuyStartDate != null)
            {
                BuyStartDate = BuyStartDate.Value.Date;
            }

            if (BuyEndDate != null)
            {
                BuyEndDate = BuyEndDate.Value.Date.AddDays(1);
            }

            if (ReturnStartDate != null)
            {
                ReturnStartDate = ReturnStartDate.Value.Date;
            }

            if (ReturnEndDate != null)
            {
                ReturnEndDate = ReturnEndDate.Value.Date.AddDays(1);
            }

            if (ReceiptStartDate.HasValue)
            {
                ReceiptStartDate = ReceiptStartDate.Value.Date;
            }

            if (ReceiptEndDate != null)
            {
                ReceiptEndDate = ReceiptEndDate.Value.Date.AddDays(1);
            }

            base.ArrangeParams();
        }

        /// <summary>
        /// 购买时间
        /// </summary>
        public DateTime? BuyStartDate { get; set; }

        /// <summary>
        ///  购买时间
        /// </summary>
        public DateTime? BuyEndDate { get; set; }

        /// <summary>
        /// 退货时间
        /// </summary>
        public DateTime? ReturnStartDate { get; set; }

        /// <summary>
        ///  退货时间
        /// </summary>
        public DateTime? ReturnEndDate { get; set; }

        /// <summary>
        /// 收货时间
        /// </summary>
        public DateTime? ReceiptStartDate { get; set; }

        /// <summary>
        /// 收货时间
        /// </summary>
        public DateTime? ReceiptEndDate { get; set; }

        /// <summary>
        /// 顾客电话
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 支付方式,目前不支持 查的是 ORDER
        /// </summary>
        public string PayType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EnumReturnGoodsStatus? ReturnGoodsStatus { get; set; }
    }

    /// <summary>
    /// RMA 收银
    /// </summary>
    public class RmaSetCashNoRequest : IStoreDataRoleRequest
    {
        public string RmaNo { get; set; }

        public string CashNo { get; set; }
        public void ArrangeParams()
        {

        }

        public int? StoreId { get; set; }
        public List<int> DataRoleStores { get; set; }
        public int? CurrentUserId { get; set; }
    }

    /// <summary>
    /// RMA 收货
    /// </summary>
    public class RmaReceiptRequest : IStoreDataRoleRequest
    {
        public string RmaNo { get; set; }

        /// <summary>
        /// 确认
        /// </summary>
        public bool? IsConfirm { get; set; }

        public void ArrangeParams()
        {

        }

        public int? StoreId { get; set; }
        public List<int> DataRoleStores { get; set; }
        public int? CurrentUserId { get; set; }
    }

    /// <summary>
    /// RMA 退货
    /// </summary>
    public class RmaReturnOfGoodsRequest : IStoreDataRoleRequest
    {
        public string RmaNo { get; set; }

        /// <summary>
        /// 确认
        /// </summary>
        public bool? IsConfirm { get; set; }

        public void ArrangeParams()
        {

        }

        public int? StoreId { get; set; }
        public List<int> DataRoleStores { get; set; }
        public int? CurrentUserId { get; set; }
    }
}