using System.Collections.Generic;

namespace Intime.OPC.Domain.Dto.Custom
{
    /// <summary>
    /// 生成销售退货单时 接收实体
    /// </summary>
    public class RMARequest
    {
        public RMARequest()
        {
            ReturnProducts=new List<KeyValuePair<int, int>>();
        }

        /// <summary>
        /// Gets or sets the store fee.
        /// </summary>
        /// <value>The store fee.</value>
        public decimal StoreFee { get; set; }

        /// <summary>
        /// Gets or sets the custom fee.
        /// </summary>
        /// <value>The custom fee.</value>
        public decimal CustomFee { get; set; }
        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        /// <value>The remark.</value>
        public string Remark { get; set; }

        /// <summary>
        /// Gets or sets the real rma sum money.
        /// </summary>
        /// <value>The real rma sum money.</value>
        public decimal RealRMASumMoney { get; set; }

        /// <summary>
        /// Gets or sets the order no.
        /// </summary>
        /// <value>The order no.</value>
        public string OrderNo { get; set; }

        /// <summary>
        /// 后台创建RMA的用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 退货明细 key ID，value 退货数量
        /// </summary>
        /// <value>The return products.</value>
        public IList<KeyValuePair<int, int>> ReturnProducts { get; set; }

      
    }
}