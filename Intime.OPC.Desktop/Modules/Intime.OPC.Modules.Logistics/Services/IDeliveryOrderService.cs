using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Modules.Logistics.Enums;
using Intime.OPC.Modules.Logistics.Models;
using Intime.OPC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Modules.Logistics.Services
{
    public interface IDeliveryOrderService : IService<OPC_ShippingSale>
    {
        /// <summary>
        /// Print delivery order or express receipt
        /// </summary>
        /// <param name="deliveryOrder"></param>
        /// <param name="receiptType"></param>
        void Print(OPC_ShippingSale deliveryOrder, ReceiptType receiptType);

        /// <summary>
        /// Complete handover
        /// </summary>
        /// <param name="deliveryOrder"></param>
        void CompleteHandOver(OPC_ShippingSale deliveryOrder);

        /// <summary>
        /// Create delivery order
        /// </summary>
        /// <param name="deliveryOrderCreationDto"></param>
        /// <returns></returns>
        OPC_ShippingSale Create(DeliveryOrderCreationDTO deliveryOrderCreationDto);
    }
}
