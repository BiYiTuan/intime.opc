using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Enums
{
    /// <summary>
    /// The associate permission.
    /// </summary>
    public enum AssociatePermission
    {
        /// <summary>
        /// All
        /// </summary>
        [Description("全部")]
        All = 0,

        /// <summary>
        /// The intime card only.
        /// </summary>
        [Description("仅有银泰卡权限")]
        IntimeCardOnly = 1,

        /// <summary>
        /// The system product.
        /// </summary>
        [Description("最高有系统商品权限的")]
        SystemProduct = 2,

        /// <summary>
        /// The self shot product.
        /// </summary>
        [Description("最高有自拍商品权限的")]
        SelfShotProduct = 3,
    }
}
