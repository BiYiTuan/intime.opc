using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;

namespace Intime.OPC.DataService.Interface.Trans
{
    public interface IRemarkService
    {
        bool WriteShippingRemark(OPC_ShipComment shipComment);
        PageResult<OPC_ShipComment> GetShipRemark(string shipId);

        bool WriteSaleRemark(OPC_SaleComment saleComment);
        PageResult<OPC_SaleComment> GetSaleRemark(string saleId);
        PageResult<OPC_OrderComment> GetOrderRemark(string orderId);
        bool WriteOrderRemark(OPC_OrderComment orderComment);
        bool WriteSaleRmaRemark(OPC_SaleRMAComment saleRmaComment);
        PageResult<OPC_SaleRMAComment> GetSaleRmaRemark(string rmaId);

        bool WriteRmaRemark(OPC_SaleRMAComment saleRmaComment);
        PageResult<OPC_SaleRMAComment> GetRmaRemark(string rmaId);

        bool WriteSaleDetailsRemark(OPC_SaleDetailsComment saleDetailsComment);
        PageResult<OPC_SaleDetailsComment> GetSaleDetailsRemark(string saleDetailId);
    }
}