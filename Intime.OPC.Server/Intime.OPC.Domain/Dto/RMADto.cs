using System;
using System.Collections.Generic;
using System.Linq;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Extensions;

namespace Intime.OPC.Domain.Dto
{
    public class RMADto
    {
        public int Id { get; set; }
        public string SaleOrderNo { get; set; }


        private string _rmano { get; set; }

        /// <summary>
        /// 退货单号
        /// </summary>
        /// <value>The rma no.</value>
        public string RMANo {
            get { return _rmano; }
            set { _rmano = value; }
        }


        public string SourceDesc { get; set; }
        /// <summary>
        /// 退货总数
        /// </summary>
        /// <value>The count.</value>
        public int? Count { get; set; }

        /// <summary>
        /// 赔偿金额
        /// </summary>
        /// <value>The refund amount.</value>
        public decimal RefundAmount { get; set; }

        public string OrderNo { get; set; }
        /// <summary>
        /// 退货类型
        /// </summary>
        /// <value>The type of the rma.</value>
        public int RMAType { get; set; }
        /// <summary>
        /// 退货单状态ID
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }

        /// <summary>
        /// 退货总金额
        /// </summary>
        /// <value>The refund amount.</value>
        public decimal RMAAmount { get; set; }

        public int? UserId { get; set; }
        /// <summary>
        /// 退货原因
        /// </summary>
        /// <value>The rma reason.</value>
        public string RMAReason { get; set; }
        public string ContactPerson { get; set; }

        private DateTime _createdDate;

        /// <summary>
        /// 要求退货时间
        /// </summary>
        /// <value>The created date.</value>
        public DateTime CreatedDate {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        public int StoreId { get; set; }
        public string StoreName { get; set; }

        /// <summary>
        /// 退货收银状态
        /// </summary>
        /// <value>The name of the rma cash status.</value>
        public string RmaCashStatusName
        {
            get { return ((EnumRMACashStatus)RmaCashStatus).GetDescription(); }
            private set { }
        }

        public int RmaCashStatus { get; set; }

        /// <summary>
        /// 退货状态
        /// </summary>
        /// <value>The name of the rma cash status.</value>
        public string RmaStatusName
        {
            get { return ((EnumReturnGoodsStatus)RmaStatus).GetDescription(); }
            private set { }
        }

        public int RmaStatus { get; set; }

        /// <summary>
        /// 退货单状态
        /// </summary>
        /// <value>The name of the status.</value>
        public string StatusName
        {
            get { return ((EnumRMAStatus)Status).GetDescription(); }
            private set { }
        }


        /// <summary>
        /// 专柜码
        /// </summary>
        /// <value>The name of the rma cash status.</value>
        public string SectionCode { get; set; }

        public string SectionName { get; set; }
        /// <summary>
        /// 收银流水号
        /// </summary>
        /// <value>The cash number.</value>
        public string CashNum { get; set; }

        /// <summary>
        /// 收银时间
        /// </summary>
        /// <value>The cash number.</value>
        public DateTime? CashDate { get; set; }

        /// <summary>
        /// 退货收银流水号
        /// </summary>
        /// <value>The cash number.</value>
        public string RmaCashNum { get; set; }

        /// <summary>
        /// 退货收银时间
        /// </summary>
        /// <value>The cash number.</value>
        public DateTime? RmaCashDate { get; set; }


        private System.DateTime? _backDate;

        /// <summary>
        /// 退货时间
        /// </summary>
        /// <value>The back date.</value>
        public System.DateTime? BackDate {
            get { return _backDate; }
            set
            {
                if (value == null)
                {
                    value = DateTime.MinValue;
                }

                _backDate = value;
            }
        }

        #region 原OPC_SaleRMA

        /// <summary>
        /// 订单来源
        /// </summary>
        public string OrderSource { get; set; }
        /// <summary>
        /// 订单运费
        /// </summary>
        public decimal? OrderTransFee { get; set; }
        public double MustPayTotal { get; set; }
        public Nullable<decimal> RealRMASumMoney { get; set; }
        public string TransMemo { get; set; }
        public DateTime BuyDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerRemark { get; set; }
        public bool IfReceipt { get; set; }
        public string ReceiptHead { get; set; }
        public string ReceiptContent { get; set; }
        /// <summary>
        /// 公司应付
        /// </summary>
        /// <value>The store fee.</value>
        public decimal? StoreFee { get; set; }
        /// <summary>
        /// 客户应付
        /// </summary>
        /// <value>The custom fee.</value>
        public decimal? CustomFee { get; set; }

        /// <summary>
        /// 客服同意时间
        /// </summary>
        /// <value>The service agree date.</value>
        public DateTime? ServiceAgreeDate { get; set; }

        /// <summary>
        /// 应退总金额[待定]
        /// </summary>
        public decimal? RecoverableSumMoney { get; set; }
        /// <summary>
        /// 赔偿 暂定[应退总金额]
        /// </summary>
        public decimal? CompensationFee { get; set; }

        #endregion

        /// <summary>
        /// 订单类型 默认为1
        /// </summary>
        public int OrderProductType { get; set; }


        private string _paymentMethodName;
        public string PaymentMethodName
        {
            get { return DefaultOrderTransaction == null ? _paymentMethodName : DefaultOrderTransaction.PaymentName; }
            set { _paymentMethodName = value; }
        }

        private string _payType;

        /// <summary>
        /// 支付方式
        /// </summary>
        /// <value>The cash number.</value>
        public string PayType
        {
            get { return DefaultOrderTransaction == null ? _payType : DefaultOrderTransaction.PaymentCode; }
            set { _payType = value; }
        }

        /// <summary>
        /// 订单支付信息
        /// </summary>
        public IEnumerable<OrderTransactionDto> OrderTransactions { get; set; }

        private OrderTransactionDto _defaultOrderTransaction;

        private OrderTransactionDto DefaultOrderTransaction
        {
            get
            {
                if (_defaultOrderTransaction != null)
                {
                    return _defaultOrderTransaction;
                }


                if (OrderTransactions != null)
                {
                    _defaultOrderTransaction = OrderTransactions.OrderByDescending(v => v.Amount)
                            .ThenByDescending(v => v.PaymentCode)
                            .FirstOrDefault();
                }

                return _defaultOrderTransaction;
            }
            set { }
        }


        #region 兼容 SALERMADTO

        /// <summary>
        /// 退货总数量
        /// </summary>
        public int RMACount { get; set; }


        /// <summary>
        /// 退貨日期
        /// </summary>
        /// <value>The create date.</value>
        public DateTime CreateDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string OrderChannelNo {
            get { return DefaultOrderTransaction == null ? String.Empty : DefaultOrderTransaction.TransNo; }
            set { }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RmaNo
        {
            get { return _rmano; }
            set { _rmano = value; }
        }

        #endregion
    }
}