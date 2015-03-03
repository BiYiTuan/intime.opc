using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;
using LinqKit;
using PredicateBuilder = System.Linq.Expressions.PredicateBuilder;

namespace Intime.OPC.Repository.Impl
{
    public class BankRepository : OPCBaseRepository<int, IMS_Bank>, IBankRepository
    {
        #region methods

        /// <summary>
        /// 筛选
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static Expression<Func<IMS_Bank, bool>> BankFilter(BankQueryRequest filter)
        {
            var query = PredicateBuilder.True<IMS_Bank>();

            if (filter == null)
            {
                return query;
            }

            if (filter.Status != null)
            {
                query = PredicateBuilder.And(query, v => v.Status == filter.Status);
            }


            if (!String.IsNullOrEmpty(filter.Code))
            {
                query = PredicateBuilder.And(query, v => v.Code == filter.Code);
                return query;
            }

            if (!String.IsNullOrEmpty(filter.Name))
            {
                query = PredicateBuilder.And(query, v => v.Name == filter.Name);

                return query;
            }

            if (!String.IsNullOrEmpty(filter.PreName))
            {
                query = PredicateBuilder.And(query, v => v.Name.StartsWith(filter.PreName));

                return query;
            }

            return query;
        }

        #endregion

        public override IEnumerable<IMS_Bank> AutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public PagerInfo<BankDto> GetPagedList(BankQueryRequest request, PagerRequest pagerRequest)
        {
            var bankFilter = BankFilter(request);

            int totalCount;
            List<IMS_Bank> datas;
            using (var db = GetYintaiHZhouContext())
            {
                var banks = db.IMS_Bank.AsExpandable().Where(bankFilter);

                var sql = from bank in banks
                          select bank;

                totalCount = sql.Count();

                datas = sql.OrderBy(v => v.Id).Skip(pagerRequest.SkipCount).Take(pagerRequest.PageSize).ToList();
            }

            var rst = AutoMapper.Mapper.Map<List<IMS_Bank>, List<BankDto>>(datas);

            return new PagerInfo<BankDto>(pagerRequest, totalCount, rst);
        }

        public BankDto GetDto(int id)
        {
            var entity = GetByID(id);

            return entity == null ? null : AutoMapper.Mapper.Map<IMS_Bank, BankDto>(entity);
        }
    }
}
