using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OPCApp.Infrastructure.Auth;

namespace OPCApp.DataService.Impl.Auth
{
    class UserAuth:UserAuth
    {
        public User CurrentUser
        {
            get; private set; 
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
    }
}
