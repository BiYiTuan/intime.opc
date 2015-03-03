using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;
using LinqKit;
using PredicateBuilder = LinqKit.PredicateBuilder;

namespace Intime.OPC.Repository.Impl
{
    public class DepartmentRepository : OPCBaseRepository<int, Department>, IDepartmentRepository
    {
        #region methods

        private static Expression<Func<Department, bool>> Filter(DepartmentQueryRequest filter)
        {
            var query = PredicateBuilder.True<Department>();

            if (filter != null)
            {
                if (!String.IsNullOrWhiteSpace(filter.NamePrefix))
                    query = PredicateBuilder.And(query, v => v.Name.StartsWith(filter.NamePrefix));

                if (!String.IsNullOrWhiteSpace(filter.Name))
                {
                    query = PredicateBuilder.And(query,
                        v => v.Name.Equals(filter.Name));
                }

                if (filter.StoreId != null)
                {
                    query = PredicateBuilder.And(query, v => v.StoreId == filter.StoreId.Value);
                }

                if (filter.StoreId == null && filter.DataRoleStores != null)
                {
                    query = PredicateBuilder.And(query, v => filter.DataRoleStores.Contains(v.StoreId));
                }
            }

            return query;
        }

        #endregion



        public override IEnumerable<Department> AutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public PagerInfo<DepartmentDto> GetPagedList(PagerRequest pagerRequest, DepartmentQueryRequest request)
        {
            var departmentFilter = Filter(request);

            using (var db = GetYintaiHZhouContext())
            {
                var departments = db.Departments;
                var stores = db.Stores;

                var q = from department in departments.AsExpandable().Where(departmentFilter)
                        join store in stores on department.StoreId equals store.Id
                        select new DepartmentDto
                            {
                                Id = department.Id,
                                Name = department.Name,
                                StoreId = store.Id,
                                StoreName = store.Name,
                                SortOrder = department.SortOrder
                            };
                var total = q.Count();

                var lst = q.OrderByDescending(v => v.SortOrder).Skip(pagerRequest.SkipCount).Take(pagerRequest.PageSize).ToList();

                return new PagerInfo<DepartmentDto>(pagerRequest, total, lst);

            }
        }

        public DepartmentDto GetDto(int id)
        {
            using (var db = GetYintaiHZhouContext())
            {
                var departments = db.Departments;
                var stores = db.Stores;

                var q = from department in departments.Where(v => v.Id == id)
                        join store in stores on department.StoreId equals store.Id
                        select new DepartmentDto
                        {
                            Id = department.Id,
                            Name = department.Name,
                            StoreId = store.Id,
                            StoreName = store.Name,
                            SortOrder = department.SortOrder
                        };

                return q.FirstOrDefault();
            }
        }
    }
}
