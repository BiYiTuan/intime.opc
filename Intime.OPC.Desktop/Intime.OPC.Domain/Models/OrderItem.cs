namespace Intime.OPC.Domain.Models
{
    public class OrderItem
    {
        public bool IsSelected { get; set; }
        public int Id { get; set; }
        public string StoreItemNo { get; set; }

        public string ColorValueName { get; set; }
        public string SizeValueName { get; set; }

        public decimal ItemPrice { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }

        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string GoodsType { get; set; }

        /// <summary>
        ///     吊牌价格
        /// </summary>
        /// <value>The unit price.</value>
        public decimal? UnitPrice { get; set; }

        /// <summary>
        ///     促销价格
        /// </summary>
        /// <value>The promotion price.</value>
        public decimal? PromotionPrice { get; set; }

        public int ProductId { get; set; }

        public int ReturnCount { get; set; }

        public int NeedReturnCount { get; set; }
        public string StoreSalesCode { get; set; }
    }
}