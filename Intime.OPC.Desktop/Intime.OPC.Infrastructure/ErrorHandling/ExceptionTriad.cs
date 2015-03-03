using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Infrastructure.ErrorHandling
{
    public class ExceptionTriad
    {
        public Exception Exception { get; set; }

        public Action CompleteCallback { get; set; }
    }
}
