using System;
using Intime.OPC.Domain.Attributes;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Domain.Customer
{
    [Uri("rmas")]
    public class RMADto : Model
    {
        private string _cashNum;
        private string _rmaCashNum;

        public string SaleOrderNo { get; set; }
        /// <summary>
        /// 退货单号
        /// </summary>
        /// <value>The rma no.</value>
        public string RMANo { get; set; }

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
        /// 退货单状态
        /// </summary>
        /// <value>The name of the status.</value>
        public string StatusName { get; set; }

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
        /// <summary>
        /// 要求退货时间
        /// </summary>
        /// <value>The created date.</value>
        public DateTime CreatedDate { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        /// <summary>
        /// 退货收银状态
        /// </summary>
        /// <value>The name of the rma cash status.</value>
        public string RmaCashStatusName { get; set; }

        /// <summary>
        /// 退货状态
        /// </summary>
        /// <value>The name of the rma cash status.</value>
        public string RmaStatusName { get; set; }

        /// <summary>
        /// 专柜码
        /// </summary>
        /// <value>The name of the rma cash status.</value>
        public string SectionCode { get; set; }

        public string SectionName { get; set; }

        /// <summary>
        /// 收银流水号
        /// </summary>
        public string CashNum
        {
            get { return _cashNum; }
            set { SetProperty(ref _cashNum, value); }
        }

        /// <summary>
        /// 收银时间
        /// </summary>
        /// <value>The cash number.</value>
        public DateTime? CashDate { get; set; }

        /// <summary>
        /// 退货收银流水号
        /// </summary>
        /// <value>The cash number.</value>
        public string RmaCashNum
        {
            get { return _rmaCashNum; }
            set { SetProperty(ref _rmaCashNum, value); }
        }

        /// <summary>
        /// 退货收银时间
        /// </summary>
        /// <value>The cash number.</value>
        public DateTime? RmaCashDate { get; set; }

        /// <summary>
        /// 退货时间
        /// </summary>
        /// <value>The back date.</value>
        public System.DateTime BackDate { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        /// <value>The cash number.</value>
        public string PayType { get; set; }

        #region 原OPC_SaleRMA
        public string PaymentMethodName { get; set; }
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
    }
}