using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain
{
    public class MyPrincipal : IPrincipal 
    {
        public IIdentity Identity
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }

    public class MyIdentity : IIdentity
    {
        public string AuthenticationType
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsAuthenticated
        {
            get { throw new NotImplementedException(); }
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }
    }

}
