using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OPCApp.Infrastructure.Auth;

namespace OPCApp.DataService.Impl.Auth
{
     class UserTokenInfo:User
    {
        public string UserID
        {
            get { throw new NotImplementedException(); }
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public string Token
        {
            get { throw new NotImplementedException(); }
        }

        public DateTime Expires
        {
            get { throw new NotImplementedException(); }
        }

        public string ConsumerKey
        {
            get { throw new NotImplementedException(); }
        }

        public string ConsumerSecret
        {
            get { throw new NotImplementedException(); }
        }
    }
}
