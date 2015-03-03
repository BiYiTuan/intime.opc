using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OPCApp.Infrastructure.Auth;

namespace OPCApp.DataService.Auth
{
    [Export(typeof(IUserAuth))]
    class UserAuth:IUserAuth
    {
        public IUser CurrentUser
        {
            get { throw new NotImplementedException(); }
        }

        public string Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public string Relogin()
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public bool CheckRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}
