using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Auth
{
    public interface IAuthUser
    {
        IEnumerable<int> AuthenticatedStores();

        bool IsAdmin { get; }
    }
}
