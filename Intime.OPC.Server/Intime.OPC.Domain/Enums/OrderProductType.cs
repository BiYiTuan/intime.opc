using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Enums
{
    /// <summary>
    /// order type
    /// </summary>
    public enum OrderProductType
    {
        None = 0,
        /// <summary>
        /// 系统
        /// </summary>
        [Description("系统")]
        System = 1,

        /// <summary>
        /// 迷你银
        /// </summary>
        [Description("迷你银")]
        MiniSilver = 2
    }
}
