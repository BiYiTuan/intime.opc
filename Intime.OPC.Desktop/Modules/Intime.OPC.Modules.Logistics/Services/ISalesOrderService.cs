using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Modules.Logistics.Services
{
    public interface ISalesOrderService : IService<OPC_Sale>
    {
        void ReceivePayment(OPC_Sale salesOrder,string paymentNumber);
    }
}
