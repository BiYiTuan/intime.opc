using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Exception
{
    public class UserNotExistException : OpcApiException
    {
        public UserNotExistException(int userid)
            : base(String.Format("用户未找到UserId:{0}", userid))
        {
            UserID = userid;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserID { get; private set; }
    }
}
