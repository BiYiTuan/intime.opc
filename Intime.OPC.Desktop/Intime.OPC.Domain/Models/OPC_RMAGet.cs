namespace Intime.OPC.Domain.Models
{
    public class OPC_RMAGet
    {
        /// <summary>
        ///     物流收货开始时间
        /// </summary>
        public string StartGoodsGetDate { get; set; }

        /// <summary>
        ///     物流收货结束时间
        /// </summary>
        public string EndGoodsGetDate { get; set; }

        /// <summary>
        ///     订单号
        /// </summary>
        public string OrderNo { get; set; }
    }
}