using System;

namespace Intime.OPC.Domain.Customer
{
    public class RmaDetail
    {
        public int Id { get; set; }
        public string RMANo { get; set; }
        public string CashNum { get; set; }
        public int? StockId { get; set; }
        public int Status { get; set; }

        /// <summary>
        /// 退货数量
        /// </summary>
        public int BackCount { get; set; }

        public decimal Price { get; set; }

        public decimal LablePrice { get; set; }

        public decimal Discount { get; set; }
        public decimal Amount { get; set; }

        /// <summary>
        /// 产品销售码 (商品编码)
        /// </summary>
        public string ProdSaleCode { get; set; }

        public bool? SalesPersonConfirm { get; set; }
        public DateTime RefundDate { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }

        /// <summary>
        /// 款号
        /// </summary>
        public string StoreItemNo { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        /// <value>The store item no.</value>
        public string SizeValueName { get; set; }

        /// <summary>
        /// 色码
        /// </summary>
        /// <value>The name of the color value.</value>
        public string ColorValueName { get; set; }

        /// <summary>
        /// 专柜码
        /// </summary>
        public string SectionCode { get; set; }

        /// <summary>
        /// 商品图片URL
        /// </summary>
        public string ProductImageUrl { get; set; }
    }
}