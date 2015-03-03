using System;

namespace Intime.OPC.Domain.Dto
{
    public class RmaDetail
    {
        public int Id { get; set; }
        public string RMANo { get; set; }
        public string CashNum { get; set; }
        public int? StockId { get; set; }
        public int Status { get; set; }

        /// <summary>
        ///     退货数量
        /// </summary>
        /// <value>The back count.</value>
        public int BackCount { get; set; }

        public decimal Price { get; set; }
        public decimal Amount { get; set; }

        /// <summary>
        /// 产品销售码 (商品编码)
        /// </summary>
        /// <value>The product sale code.</value>
        public string ProdSaleCode { get; set; }

        public bool? SalesPersonConfirm { get; set; }
        public DateTime RefundDate { get; set; }

        public int BrandId { get; set; }

        public string BrandName { get; set; }

        /// <summary>
        ///     款号
        /// </summary>
        /// <value>The store item no.</value>
        public string StoreItemNo { get; set; }

        /// <summary>
        ///     规格
        /// </summary>
        /// <value>The store item no.</value>
        public string SizeValueName { get; set; }

        /// <summary>
        ///     色码
        /// </summary>
        /// <value>The name of the color value.</value>
        public string ColorValueName { get; set; }

        /// <summary>
        /// 专柜码
        /// </summary>
        /// <value>The name of the color value.</value>
        public string SectionCode { get; set; }

        /// <summary>
        /// 销售原价
        /// </summary>
        public decimal LablePrice { get; set; }

        public decimal Discount { get; set; }

    }
}