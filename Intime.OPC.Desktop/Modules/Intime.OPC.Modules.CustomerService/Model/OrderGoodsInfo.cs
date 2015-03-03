/// <summary>
/// The Model namespace.
/// </summary>

namespace OPCApp.Order.Model
{
    /// <summary>
    ///     商品明细
    /// </summary>
    public class OrderGoodsInfo : ModelBase
    {
        public string SaleOrderID { get; set; }

        public string Brand { get; set; }

        public string Style { get; set; }

        public string Standard { get; set; }
        public string Color { get; set; }

        public double BuyNumber { get; set; }

        public double BuyPrice { get; set; }

        public double BackNumber { get; set; }

        public double OriginalPrice { get; set; }
        public double Price { get; set; }
    }
}