using System;
using Intime.OPC.Domain.Attributes;

namespace Intime.OPC.Domain.Models
{
    /// <summary>
    /// 部门
    /// </summary>
    [Uri("departments")]
    public class Department : Dimension
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public int StoreID { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string StoreName { get; set; }
    }
}
