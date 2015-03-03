using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Enums
{
    /// <summary>
    /// copy yintai.hangzhou
    /// </summary>
    public enum InviteCodeRequestStatus
    {
        [Description("null")]
        None = 0,
        [Description("申请")]
        Requesting = 1,
        [Description("批准")]
        Approved = 10,
        [Description("拒绝")]
        Reject = 99,
    }
}
