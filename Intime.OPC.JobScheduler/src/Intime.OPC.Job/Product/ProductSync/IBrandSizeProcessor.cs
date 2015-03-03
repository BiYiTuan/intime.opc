using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Repository.DTO;

namespace Intime.OPC.Job.Product.ProductSync
{
    interface IBrandSizeProcessor
    {
        void Process(ProductDto product, Inventory inventory);
    }
}
