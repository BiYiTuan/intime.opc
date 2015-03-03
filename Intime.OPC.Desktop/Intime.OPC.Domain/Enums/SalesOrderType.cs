using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Enums
{
    /// <summary>
    /// 销售单类型
    /// </summary>
    public enum SalesOrderType
    {
        None = 0,
        /// <summary>
        /// 系统
        /// </summary>
        [Description("系统商品")]
        System = 1,

        /// <summary>
        /// 迷你银
        /// </summary>
        [Description("迷你银商品")]
        MiniIntime = 2
    }
}
