using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Service.Contract
{
    public interface IExpressService
    {
        void CreatePackage(ShippingSaleCreateDto package, int uid);

        PageResult<OPC_ShippingSale> QueryShippingSales(ExpressRequestDto request, int shippingStatus, int uid);
    }
}
