namespace Intime.OPC.Domain.Customer
{
    public class OrderItemDto
    {
        public bool IsSelected { get; set; }

        public int Id { get; set; }
        /// <summary>
        ///款号
        /// </summary>
        /// <value>The store item no.</value>
        public string StoreItemNo { get; set; }

        public string ColorValueName { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        /// <value>The name of the size value.</value>
        public string SizeValueName { get; set; }

        /// <summary>
        /// 购买时的价格
        /// </summary>
        /// <value>The item price.</value>
        public decimal ItemPrice { get; set; }

        /// <summary>
        /// 购买数量
        /// </summary>
        /// <value>The quantity.</value>
        public int Quantity { get; set; }

        public int BrandId { get; set; }
        public string BrandName { get; set; }

        /// <summary>
        ///     零售价格
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