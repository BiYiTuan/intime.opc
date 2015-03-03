using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Infrastructure.Service
{
    public interface IAddtionalService
    {

        void Update<TData>(string uri, TData data);

        void Update(string uri);
    }
}
