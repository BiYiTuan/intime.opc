using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Exception
{
    /// <summary>
    ///     Class OrderNotExistsException.
    /// </summary>
    public class RoleNotExistsExcepion : OpcApiException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RoleNotExistsExcepion" /> class.
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        public RoleNotExistsExcepion(int roleId)
            : base(string.Format("角色不存在,角色Id：{0}", roleId))
        {
            this.RoleId = roleId.ToString();
        }

        public string RoleId { get; private set; }
    }
}
