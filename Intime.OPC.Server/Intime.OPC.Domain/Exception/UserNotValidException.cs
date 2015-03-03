using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Exception
{
    public class UserNotValidException : UserException
    {
        public UserNotValidException(int userid)
            : base(String.Format("用户UserId:{0}未通过验证", userid))
        {
            UserID = userid;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserID { get; private set; }
    }


    public class UserException : OpcApiException
    {
        public UserException(string msg)
            : base(msg)
        {
        }
    }
}
