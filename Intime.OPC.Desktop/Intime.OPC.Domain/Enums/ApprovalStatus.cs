using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Enums
{
    /// <summary>
    /// 审核状态
    /// </summary>
    public enum ApprovalStatus
    {
        [Description("未知")]
        None = 0,

        [Description("申请中")]
        Requesting = 1,

        [Description("已批准")]
        Approved = 10,

        [Description("已拒绝")]
        Reject = 99,
    }
}
