using System;
using System.Collections.Generic;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Models
{
    [System.Obsolete("OPC_SaleRMA 过期 请使用OPC_RMA")]
    public partial class OPC_SaleRMA:IEntity
    {
        public int Id { get; set; }
        public string SaleOrderNo { get; set; }
        public string Reason { get; set; }
        public System.DateTime BackDate { get; set; }
        /// <summary>
        /// 退货单状态
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int UpdatedUser { get; set; }
        public Nullable<decimal> StoreFee { get; set; }
        public Nullable<decimal> CustomFee { get; set; }
        public string RMAMemo { get; set; }
        /// <summary>
        /// 退货总金额
        /// </summary>
        /// <value>The compensation fee.</value>
        public Nullable<decimal> CompensationFee { get; set; }
        public Nullable<int> RMACount { get; set; }
        public Nullable<int> SectionId { get; set; }
        public string OrderNo { get; set; }
        public string SaleRMASource { get; set; }
        /// <summary>
        /// 退货状态
        /// </summary>
        /// <value>The rma status.</value>
        public int RMAStatus { get; set; }
        public int RMACashStatus { get; set; }
        public string RMANo { get; set; }

        public int StoreId { get; set; }
        /// <summary>
        /// 实退总金额
        /// </summary>
        /// <value>The real rma sum money.</value>
        public Nullable<decimal> RealRMASumMoney { get; set; }

        public DateTime? ServiceAgreeTime { get; set; }

        /// <summary>
        /// 赔偿
        /// </summary>
        /// <value>The recoverable sum money.</value>
        public Decimal? RecoverableSumMoney { get; set; }
    }
}
