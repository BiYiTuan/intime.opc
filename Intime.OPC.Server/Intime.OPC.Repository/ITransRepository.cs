using System;
using System.Collections.Generic;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository
{
    public interface ITransRepository:IRepository<OPC_Sale>
    {
        PageResult<OPC_Sale> Select(DateTime startDate, DateTime endDate, string orderNo, string saleOrderNo, int pageIndex, int pageSize = 20);
        IList<OPC_SaleDetail> SelectSaleDetail(IEnumerable<string> saleNos);

        //ResultMsg InputRemark(string strID, string remark, string strType);
    }
}