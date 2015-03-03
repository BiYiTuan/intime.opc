using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Service
{
    public interface IShippingSaleService:IService
    {
        PageResult<OPC_ShippingSale> GetByShippingCode(string shippingCode, int pageIndex, int pageSize = 20);

        void Shipped(string saleOrderNo,int userID);

        void PrintExpress(string orderNo, int userId);

        /// <summary>
        /// 创建退货快递单
        /// </summary>
        /// <param name="rmaNo">The rma no.</param>
        void CreateRmaShipping(string rmaNo, int userId);

        /// <summary>
        ///设定快递公司
        /// </summary>
        /// <param name="request">The request.</param>
        void UpdateRmaShipping(RmaExpressSaveDto request);

        void PintRmaShippingOver(string shippingCode);

        void PintRmaShipping(string shippingCode);

        PageResult<ShippingSaleDto> GetRmaByPackPrintPress(RmaExpressRequest request);
        PageResult<ShippingSaleDto> GetRmaShippingPrintedByPack(RmaExpressRequest request);
        void PintRmaShippingOverConnect(string shippingCode);


        PageResult<ShippingSaleDto> GetPagedList(GetShippingSaleOrderRequest request);

    }
}
