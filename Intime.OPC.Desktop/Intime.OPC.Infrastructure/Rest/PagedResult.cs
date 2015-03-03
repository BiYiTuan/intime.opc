using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Intime.OPC.Infrastructure.REST
{
    /// <summary>
    /// 数据响应
    /// </summary>
    /// <typeparam name="TData">响应数据类型</typeparam>
    [DataContract]
    public class PagedResult<TEntity>
    {
        /// <summary>
        /// 当前页
        /// </summary>
        [DataMember(Name = "pageindex")]
        public int PageIndex { get; set; }

        /// <summary>
        /// 页面大小
        /// </summary>
        [DataMember(Name = "pagesize")]
        public int PageSize { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        [DataMember(Name = "totalcount")]
        public int TotalCount { get; set; }

        /// <summary>
        /// 响应数据
        /// </summary>
        [DataMember(Name = "datas")]
        public IList<TEntity> Data { get; set; }
    }
}
