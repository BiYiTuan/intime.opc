using System.Collections.Generic;

namespace Intime.OPC.Domain.Dto.Request
{
    public interface IStoreDataRoleRequest : IArrangeParamsRequest
    {
        /// <summary>
        /// storeid
        /// </summary>
        int? StoreId { get; set; }

        /// <summary>
        /// 数据权限
        /// </summary>
        List<int> DataRoleStores { get; set; }

        /// <summary>
        /// 当前认证后的UserID
        /// </summary>
        int? CurrentUserId { get; set; }
    }
}