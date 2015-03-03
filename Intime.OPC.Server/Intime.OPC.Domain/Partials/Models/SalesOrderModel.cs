using System;
using System.Collections.Generic;
using System.Linq;

namespace Intime.OPC.Domain.Models
{
    /// <summary>
    /// 订单支付信息
    /// </summary>
    public class OrderTransactionModel
    {
        public string TransNo { get; set; }

        public string PaymentCode { get; set; }

        public string PaymentName { get; set; }

        public decimal Amount { get; set; }
    }


    public class SalesOrderModel
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


        public int SalesType { get; set; }


        public int? ShipViaId { get; set; }


        public int Status { get; set; }

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

        //public string ShippingStatusName { get; set; }

        /// <summary>
        /// 物流备注
        /// </summary>

        public string ShippingRemark { get; set; }


        public DateTime SellDate { get; set; }


        public bool? IfTrans { get; set; }


        public int? TransStatus { get; set; }


        public decimal SalesAmount { get; set; }


        public int? SalesCount { get; set; }


        public int? CashStatus { get; set; }


        public string CashNum { get; set; }


        public DateTime? CashDate { get; set; }


        public int? SectionId { get; set; }


        public int? PrintTimes { get; set; }


        public string Remark { get; set; }


        public DateTime? RemarkDate { get; set; }


        public DateTime CreatedDate { get; set; }


        public int? CreatedUser { get; set; }


        public DateTime? UpdatedDate { get; set; }


        public int? UpdatedUser { get; set; }


        // public string StatusName { get; set; }


        public string CashStatusName { get; set; }


        public string StoreName { get; set; }


        public string InvoiceSubject { get; set; }

        public string SectionName { get; set; }

        public bool? NeedInvoice { get; set; }

        /// <summary>
        /// 发票详情
        /// </summary>
        public string Invoice { get; set; }

        public string StoreTelephone { get; set; }


        public string StoreAddress { get; set; }


        public string OrderSource { get; set; }


        public string ReceivePerson { get; set; }


        public string CustomerName { get; set; }


        public string CustomerPhone { get; set; }


        public int StoreId { get; set; }


        public string SectionCode { get; set; }

        public int? ShippingSaleId { get; set; }

        /// <summary>
        /// 订单类型 2 迷你银
        /// </summary>
        public int? OrderProductType { get; set; }

        /// <summary>
        /// 用户备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 促销规则
        /// </summary>
        public string PromotionRules { get; set; }

        /// <summary>
        /// 促销描述
        /// </summary>
        public string PromotionDesc { get; set; }


        private string _transno;
        /// <summary>
        /// 渠道（支付号）
        /// </summary>
        public string TransNo
        {
            get { return OrderTransactionModel == null ? _transno : OrderTransactionModel.TransNo; }
            private set {  }
        }

        private string _paytype;
        /// <summary>
        /// 支付方式 （payment.name）
        /// </summary>
        public string PayType { get { return OrderTransactionModel == null ? _paytype : OrderTransactionModel.PaymentName; } private set { } }

        /// <summary>
        /// 订单支付信息
        /// </summary>
        public IEnumerable<OrderTransactionModel> OrderTransactionModels { get; set; }


        private OrderTransactionModel _defaultOrderTransactionModel;

        protected OrderTransactionModel OrderTransactionModel
        {
            get
            {
                if (_defaultOrderTransactionModel != null)
                {
                    return _defaultOrderTransactionModel;
                }


                if (OrderTransactionModels != null)
                {
                    _defaultOrderTransactionModel = OrderTransactionModels.OrderByDescending(v => v.Amount)
                            .ThenByDescending(v => v.PaymentCode)
                            .FirstOrDefault();
                }

                return _defaultOrderTransactionModel;
            }
            private set { }
        }
    }
}
