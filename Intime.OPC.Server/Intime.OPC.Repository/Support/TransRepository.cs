using Intime.OPC.Domain;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intime.OPC.Repository.Support
{
    public class TransRepository : BaseRepository<OPC_Sale>, ITransRepository
    {
        #region ITransRepository Members

        public PageResult<OPC_Sale> Select(DateTime startDate, DateTime endDate, string orderNo, string saleOrderNo, int pageIndex, int pageSize = 20)
        {
            using (var db = new YintaiHZhouContext())
            {
                var filterExpression =
                    db.OPC_Sales.Where(t => t.SellDate >= startDate && t.SellDate < endDate)
                        .Join(
                            db.Sections.Where(
                                s =>
                                    s.Status != -1 && s.StoreId.HasValue &&
                                    CurrentUser.StoreIds.Contains(s.StoreId.Value)), o => o.SectionId, s => s.Id,
                            (o, s) => o);

                if (!string.IsNullOrEmpty(orderNo))
                {
                    filterExpression = filterExpression.Where(p => p.OrderNo.Contains(orderNo));
                }
                if (!string.IsNullOrEmpty(saleOrderNo))
                {
                    filterExpression = filterExpression.Where(p => p.SaleOrderNo.Contains(saleOrderNo));
                }

                if (CurrentUser != null)
                {
                    var ll = CurrentUser.SectionIds;
                    filterExpression = filterExpression.Where(t => t.SectionId.HasValue && ll.Contains(t.SectionId.Value));
                }

                filterExpression = filterExpression.OrderByDescending(t => t.CreatedDate);
                return filterExpression.ToPageResult(pageIndex, pageSize);
            }
        }

        public bool Finish(Dictionary<string, string> sale)
        {
            using (var db = new YintaiHZhouContext())
            {
                int intid = int.Parse(sale["id"]);
                int intstatus = int.Parse(sale["status"]);

                OPC_Sale user = db.OPC_Sales.Where(e => e.Id == intid).FirstOrDefault();
                if (user != null)
                {
                    user.Status = intstatus;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        ///     查询销售单明细信息
        /// </summary>
        /// <param name="saleIDs">销售单ID串</param>
        /// <returns></returns>
        public IList<OPC_SaleDetail> SelectSaleDetail(IEnumerable<string> saleNos)
        {
            using (var db = new YintaiHZhouContext())
            {
                return db.OPC_SaleDetails.Where(t => saleNos.Contains(t.SaleOrderNo)).ToList();
            }
        }

        #endregion
    }
}