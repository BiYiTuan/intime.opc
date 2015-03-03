using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Service.Contract
{
    public interface ISaleOrderService
    {
        IList<SaleDetailDto> GetSaleDetails(string saleOrderNo);

        IList<OPC_SaleComment> GetSaleComments(string saleOrderNo,int uid);

        void CommentSaleOrder(string comment, string saleOrderNo, int uid);

        IList<OPC_Sale> GetSaleOrdersByPachageId(int packageId);

        IList<SaleDto> QuerySaleOrders(SaleOrderQueryRequest request, int uid);


        /// <summary>
        /// 获取分页LIST
        /// </summary>
        /// <param name="request"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        PagerInfo<SaleDto> GetPagedList(GetSaleOrderQueryRequest request, int uid);

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="salesorderno"></param>
        /// <returns></returns>
        SaleDto GetItem(string salesorderno);
    }
}
