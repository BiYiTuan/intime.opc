using Intime.OPC.Modules.Logistics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Modules.Logistics.Services
{
    public class InvoiceService : IInvoiceService
    {
        public List<Invoice> SearchData(Invoice4Get invoice4get)
        {
            var invoice = new List<Invoice>();
            return invoice;
        }

        public ResultMsg SetFinish(int type)
        {
            var msg = new ResultMsg();
            return msg;
        }
    }
}
