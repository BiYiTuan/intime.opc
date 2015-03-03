using System;
using System.Runtime.Serialization;

namespace Intime.OPC.Domain.Dto
{
    [DataContract]
    public class SaleDto
    {
        /// <summary>
        /// Id 主键ID
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 订单 NO
        /// </summary>
        [DataMember]
        public string OrderNo { get; set; }

        /// <summary>
        /// 销售单 NO
        /// </summary>
        [DataMember]
        public string SaleOrderNo { get; set; }

        [DataMember]
        public int SalesType { get; set; }

        [DataMember]
        public int? ShipViaId { get; set; }

        [DataMember]
        public int Status { get; set; }

        /// <summary>
        /// 物流单 code
        /// </summary>
        [DataMember]
        public string ShippingCode { get; set; }

        /// <summary>
        /// 物流 费
        /// </summary>
        [DataMember]
        public decimal ShippingFee { get; set; }

        /// <summary>
        /// 物流 状态
        /// </summary>
        [DataMember]
        public int? ShippingStatus { get; set; }

        /// <summary>
        /// 物流状态 Name
        /// </summary>
        [DataMember]
        public string ShippingStatusName { get; set; }

        /// <summary>
        /// 物流备注
        /// </summary>
        [DataMember]
        public string ShippingRemark { get; set; }

        [DataMember]
        public DateTime SellDate { get; set; }

        [DataMember]
        public string IfTrans { get; set; }

        [DataMember]
        public string TransStatus { get; set; }

        [DataMember]
        public decimal SalesAmount { get; set; }

        [DataMember]
        public int? SalesCount { get; set; }

        [DataMember]
        public int? CashStatus { get; set; }

        [DataMember]
        public string CashNum { get; set; }

        [DataMember]
        public DateTime? CashDate { get; set; }

        [DataMember]
        public int? SectionId { get; set; }

        [DataMember]
        public int? PrintTimes { get; set; }

        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public DateTime? RemarkDate { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public int CreatedUser { get; set; }

        [DataMember]
        public DateTime UpdatedDate { get; set; }

        [DataMember]
        public int UpdatedUser { get; set; }

        [DataMember]
        public string StatusName { get; set; }

        [DataMember]
        public string CashStatusName { get; set; }

        [DataMember]
        public string StoreName { get; set; }

        [DataMember]
        public string InvoiceSubject { get; set; }

        [DataMember]
        public string PayType { get; set; }

        [DataMember]
        public string SectionName { get; set; }

        [DataMember]
        public string NeedInvoice { get; set; }

        /// <summary>
        /// 发票详情
        /// </summary>
        [DataMember]
        public string Invoice { get; set; }

        [DataMember]
        public string StoreTelephone { get; set; }

        [DataMember]
        public string StoreAddress { get; set; }

        [DataMember]
        public string TransNo { get; set; }

        [DataMember]
        public string OrderSource { get; set; }

        [DataMember]
        public string ReceivePerson { get; set; }


        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string CustomerPhone { get; set; }

        [DataMember]
        public int StoreId { get; set; }
        [DataMember]
        public string SectionCode { get; set; }

        [DataMember]
        public int ShippingSaleId { get; set; }


        private int _orderProductType = 1;

        /// <summary>
        /// 订单类型 2 迷你银
        /// </summary>
        [DataMember]
        public int? OrderProductType
        {
            get { return _orderProductType; }
            set
            {
                if (value == null)
                {
                    value = 1;
                }

                _orderProductType = value.Value;
            }
        }


        /// <summary>
        /// 用户备注
        /// </summary>
        [DataMember]
        public string Memo { get; set; }

        /// <summary>
        /// 促销规则
        /// </summary>
        [DataMember]
        public string PromotionRules { get; set; }

        /// <summary>
        /// 促销描述
        /// </summary>
        [DataMember]
        public string PromotionDesc { get; set; }
    }
}