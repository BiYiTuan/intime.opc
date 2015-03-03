using Intime.OPC.Domain.Models;

namespace Intime.OPC.Job.Product
{
    public interface IInventorySyncProcessor
    {
        Inventory Sync(OPC_SKU sku);
    }
}
