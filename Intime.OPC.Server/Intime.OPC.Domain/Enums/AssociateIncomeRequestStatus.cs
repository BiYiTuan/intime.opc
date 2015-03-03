using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Enums
{
    /// <summary>
    /// 合伙人提现申请状态
    /// </summary>
    public enum AssociateIncomeRequestStatus
    {
        Requesting = 1,
        Transferring = 2,
        /// <summary>
        /// 已转账成功的
        /// </summary>
        [Description("已转账成功")]
        Transferred = 3,
        Failed = 4
    }
}
