using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Enums
{
    /// <summary>
    /// 用户的等级 copy https://github.com/victorgong/ytoo.service/blob/master/Yintai.Hangzhou.Model/Enums/UserLevel.cs
    /// </summary>
    [Flags]
    public enum UserLevel
    {
        /// <summary>
        /// 啥都不是
        /// </summary>
        [Description("默认")]
        None = 0,

        /// <summary>
        /// 普通用户
        /// </summary>
        [Description("普通用户")]
        User = 1,

        /// <summary>
        /// 达人
        /// </summary>
        [Description("达人")]
        Daren = 2,

        [Description("导购")]
        DaoGou = 4

    }
}
