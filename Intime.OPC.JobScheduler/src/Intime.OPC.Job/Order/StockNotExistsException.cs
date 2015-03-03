using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Job.Order
{
     public class StockNotExistsException:Exception
    {
         public StockNotExistsException(string message) : base(message) { }
    }
}
