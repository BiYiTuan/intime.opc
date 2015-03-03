using Intime.OPC.Domain.Attributes;
using OPCAPP.Domain.Dto.Financial;

namespace Intime.OPC.Domain.Dto.Financial
{
    [Uri("statistics/cashierdetailsreport")]
    public class WebSiteCashierSearchDto : WebSiteBaseDto
    {
        public int Count { get; set; }
        /// <summary>
        ///     退货单号
        /// </summary>
        /// <value>The rma no.</value>
        public string RMANo { get; set; }

        /// <summary>
        ///     收银流水号
        /// </summary>
        /// <value>The cash number.</value>
        public string CashNum { get; set; }

        /// <summary>
        ///     退货收银流水号
        /// </summary>
        /// <value>The cash number.</value>
        public string RmaCashNum { get; set; }

        /// <summary>
        /// 销售单号
        /// </summary>
        public string SalesOrderNo { get; set; }

        /// <summary>
        /// 销售金额
        /// </summary>
        public decimal SaleTotalPrice { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string DetailType { get; set; }
    }
}