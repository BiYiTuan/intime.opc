using System.Collections.Generic;
using Intime.OPC.Domain.Dto.Request;

namespace Intime.OPC.Domain.Dto.Financial
{
    public class SearchStatRequest : DatePageRequest
    {
        public SearchStatRequest()
            : base(2000)
        {
        }

        public SearchStatRequest(int maxPageSize = 2000)
            : base(maxPageSize)
        {
        }

        /// <summary>
        /// 数据层面的权限 为NULL 不验证
        /// </summary>
        public List<int> DataRoleStores { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public int? StoreId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 销售单号
        /// </summary>
        public string SalesOrderNo { get; set; }

        /// <summary>
        /// 渠道单号
        /// </summary>
        public string OrderChannelNo { get; set; }

        /// <summary>
        /// 退货单号
        /// </summary>
        public string RMANo { get; set; }

        public override void ArrangeParams()
        {
            StoreId = CheckIsNullOrAndSet(StoreId);

            base.ArrangeParams();
        }
    }
}