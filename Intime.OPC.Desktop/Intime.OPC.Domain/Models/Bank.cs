using System;
using Intime.OPC.Domain.Attributes;

namespace Intime.OPC.Domain.Models
{
    /// <summary>
    /// 银行
    /// </summary>
    [Uri("banks")]
    public class Bank : Model
    {
        /// <summary>
        /// 银行名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 银行代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
