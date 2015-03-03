using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Enums.SortOrder;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;

namespace Intime.OPC.Repository.Impl
{
    public class SupplierRepository : OPCBaseRepository<int, OpcSupplierInfo>, ISupplierRepository
    {
        #region methods

        private static Expression<Func<OpcSupplierInfo, bool>> Filter(SupplierFilter filter)
        {
            var query = PredicateBuilder.True<OpcSupplierInfo>();

            if (filter != null)
            {
                if (!String.IsNullOrWhiteSpace(filter.NamePrefix))
                    query = PredicateBuilder.And(query, v => v.SupplierName.StartsWith(filter.NamePrefix));

                if (filter.Status != null)
                {
                    query = PredicateBuilder.And(query, v => v.Status == filter.Status.Value);
                }
                else
                {
                    query = PredicateBuilder.And(query, v => v.Status > 0);
                }

                if (!String.IsNullOrWhiteSpace(filter.Name))
                {
                    query = PredicateBuilder.And(query, v => v.SupplierName == filter.Name);
                }
            }

            return query;
        }

        private static Func<IQueryable<OpcSupplierInfo>, IOrderedQueryable<OpcSupplierInfo>> OrderBy(SupplierSortOrder sortOrder)
        {
            Func<IQueryable<OpcSupplierInfo>, IOrderedQueryable<OpcSupplierInfo>> orderBy = null;

            switch (sortOrder)
            {
                default:
                    //orderBy = v => v.OrderByDescending(s => s);
                    break;
            }

            return orderBy;
        }

        #endregion

        protected override DbQuery<OpcSupplierInfo> DbQuery(DbContext context)
        {
            return context.Set<OpcSupplierInfo>(); ;

        }

        public override IEnumerable<OpcSupplierInfo> AutoComplete(string query)
        {
            var filter = Filter(new SupplierFilter
            {
                NamePrefix = query
            });

            return Func(v => EFHelper.Get(DbQuery(v), filter, null, 20).ToList());
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pagerRequest"></param>
        /// <param name="totalCount"></param>
        /// <param name="filter"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<OpcSupplierInfo> GetPagedList(PagerRequest pagerRequest, out int totalCount, SupplierFilter filter, SupplierSortOrder sortOrder)
        {
            var query = Filter(filter);
            var orderBy = OrderBy(sortOrder);
            var result = Func(c =>
            {
                int t;
                var q1 = EFHelper.GetPaged(c, query, out t, pagerRequest.PageIndex, pagerRequest.PageSize, orderBy);

                var q2 = from b in c.Set<Brand>()
                         join sb in c.Set<Supplier_Brand>() on b.Id equals sb.Brand_Id into temp1
                         from sb in temp1.DefaultIfEmpty()
                         select new
                         {
                             b,
                             sb.Supplier_Id
                         };
                var q = from s in q1
                        join b in q2 on s.Id equals b.Supplier_Id into temp1
                        from sb in temp1.DefaultIfEmpty()
                        select new
                        {
                            Et = s,
                            Brand = sb.b

                        };

                var list = new Dictionary<int, OpcSupplierInfo>();

                var rst = q.ToList();

                rst.ForEach(v =>
                {
                    Brand b = null;
                    if (v.Brand != null)
                    {
                        b = Brand.Convert2Brand(v.Brand);
                    }

                    var item = OpcSupplierInfo.Convert2Supplier(v.Et);

                    OpcSupplierInfo s;
                    if (list.TryGetValue(item.Id, out s))
                    {
                        if (b != null)
                        {
                            var l = s.Brands.ToList();
                            l.Add(b);

                            s.Brands = l;
                        }
                    }
                    else
                    {
                        if (b != null)
                        {
                            var l = item.Brands.ToList();
                            l.Add(b);

                            item.Brands = l;
                        }

                        list.Add(item.Id, item);
                    }

                });

                return new
                {
                    totalCount = t,
                    Data = list.Values.ToList()
                };
            });

            totalCount = result.totalCount;

            return result.Data;
        }

        public override void Update(OpcSupplierInfo entity)
        {
            Action(c =>
            {
                using (var trans = new TransactionScope())
                {
                    Supplier_Brand[] supplier_brands = null;
                    if (entity.Brands != null && entity.Brands.Count() != 0)
                    {
                        supplier_brands = entity.Brands.Select(v => new Supplier_Brand()
                        {
                            Brand_Id = v.Id,
                            Supplier_Id = entity.Id
                        }).ToArray();

                        entity.Brands = null;
                    }

                    EFHelper.Update(c, entity);

                    if (supplier_brands == null)
                    {
                        //del 
                        EFHelper.Delete<Supplier_Brand>(c, v => v.Supplier_Id == entity.Id);
                    }
                    else
                    {
                        var list = EFHelper.Get<Supplier_Brand>(c, v => v.Supplier_Id == entity.Id).ToList();
                        if (list.Count == 0)
                        {
                            EFHelper.Inserts<Supplier_Brand>(c, supplier_brands);
                        }
                        else
                        {
                            //1 加的 2 删的
                            var l_b = list.Select(v => v.Brand_Id);
                            var s_b = supplier_brands.Select(v => v.Brand_Id);
                            var b = s_b.Except(l_b).ToArray();//新增的，
                            var a = l_b.Except(s_b).ToArray();//剔除的

                            if (b.Length > 0)
                            {
                                foreach (var i in b)
                                {
                                    EFHelper.Insert<Supplier_Brand>(c, new Supplier_Brand
                                    {
                                        Brand_Id = i,
                                        Supplier_Id = entity.Id
                                    });
                                }
                            }

                            if (a.Length > 0)
                            {
                                foreach (var i in a)
                                {
                                    EFHelper.Delete<Supplier_Brand>(c, v => v.Supplier_Id == entity.Id && v.Brand_Id == i);
                                }
                            }

                        }

                    }

                    trans.Complete();
                }
            });
        }

        public override OpcSupplierInfo Insert(OpcSupplierInfo entity)
        {
            //insert 关系
            return Func<OpcSupplierInfo>(c =>
            {
                using (var trans = new TransactionScope())
                {

                    Supplier_Brand[] supplier_brands = null;
                    if (entity != null && entity.Brands.Count() != 0)
                    {
                        supplier_brands = entity.Brands.Select(v => new Supplier_Brand()
                        {
                            Brand_Id = v.Id,
                            Supplier_Id = 0
                        }).ToArray();

                        entity.Brands = null;
                    }

                    var item = EFHelper.Insert(c, entity);

                    if (supplier_brands != null)
                    {
                        supplier_brands.ForEach(v => v.Supplier_Id = item.Id);

                        EFHelper.InsertOrUpdate<Supplier_Brand>(c, supplier_brands.ToArray());
                    }

                    trans.Complete();
                    return item;
                }
            });
        }

        public override OpcSupplierInfo GetItem(int key)
        {
            return Func(c =>
            {

                //var brands =
                //    Supplier_Brand.Join(Brand, o => o.Brand_Id, i => i.Id, (o, i) => new { Brand = i, o.Supplier_Id })
                //        .Where(v => v.Supplier_Id == key);


                var q1 = EFHelper.Get<OpcSupplierInfo>(c, v => v.Id == key);

                var q2 = from b in c.Set<Brand>()
                         join sb in c.Set<Supplier_Brand>() on b.Id equals sb.Brand_Id into temp1
                         from sb in temp1.DefaultIfEmpty()
                         select new
                         {
                             b,
                             sb.Supplier_Id
                         };
                var q = from s in q1
                        join b in q2 on s.Id equals b.Supplier_Id into temp1
                        from sb in temp1.DefaultIfEmpty()
                        select new
                        {
                            Et = s,
                            B = sb.b
                        };

                var rst = q.ToList();

                var list = new Dictionary<int, OpcSupplierInfo>();

                rst.ForEach(v =>
                {
                    Brand b = null;
                    if (v.B != null)
                    {
                        b = Brand.Convert2Brand(v.B);

                    }

                    var item = OpcSupplierInfo.Convert2Supplier(v.Et);

                    OpcSupplierInfo s;
                    if (list.TryGetValue(item.Id, out s))
                    {
                        if (b != null)
                        {
                            var l = s.Brands.ToList();
                            l.Add(b);

                            s.Brands = l;
                        }
                    }
                    else
                    {
                        if (b != null)
                        {
                            var l = item.Brands.ToList();
                            l.Add(b);

                            item.Brands = l;
                        }

                        list.Add(item.Id, item);
                    }

                });

                return list.Values.FirstOrDefault();
            });
        }
    }
}