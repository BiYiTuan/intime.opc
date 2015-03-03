using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto.Financial;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;

namespace Intime.OPC.Repository.Support
{
    public class SaleDetailRepository:BaseRepository<OPC_SaleDetail>, ISaleDetailRepository
    {
        public PageResult<OPC_SaleDetail> GetBySaleOrderNo(string saleOrderNo, int pageIndex, int pageSize)
        {
            return  Select2<OPC_SaleDetail,DateTime>(t => t.SaleOrderNo == saleOrderNo,t=>t.CreatedDate,false,pageIndex,pageSize);
        }

        public PageResult<OPC_SaleDetail> GetByOrderNo(string orderNo, int pageIndex, int pageSize)
        {
            using (var db = new YintaiHZhouContext())
            {
                var pp= db.OPC_Sales.Where(t => t.OrderNo == orderNo).Join(db.OPC_SaleDetails, t => t.SaleOrderNo,
                    o => o.SaleOrderNo, (t, o) => o).OrderByDescending(t=>t.CreatedDate);
                return pp.ToPageResult(pageIndex, pageSize);
            }
        }
    }
}
