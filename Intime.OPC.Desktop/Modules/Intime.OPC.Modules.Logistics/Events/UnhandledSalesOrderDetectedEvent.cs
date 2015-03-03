using Microsoft.Practices.Prism.PubSubEvents;
using Intime.OPC.Modules.Logistics.Models;

namespace Intime.OPC.Modules.Logistics.Events
{
    public class UnhandledSalesOrderDetectedEvent : PubSubEvent<Invoice4Get>
    {
    }
}
