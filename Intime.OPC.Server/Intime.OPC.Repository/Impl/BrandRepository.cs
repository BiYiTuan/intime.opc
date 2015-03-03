// ***********************************************************************
// Assembly         : 01_Intime.OPC.Repository
// Author           : Liuyh
// Created          : 03-28-2014 21:10:15
//
// Last Modified By : Liuyh
// Last Modified On : 03-28-2014 21:10:30
// ***********************************************************************
// <copyright file="BrandRepository.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Enums.SortOrder;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;
using LinqKit;
using PredicateBuilder = LinqKit.PredicateBuilder;

namespace Intime.OPC.Repository.Impl
{
    /// <summary>
    /// Class BrandRepository.
    /// </summary>
    public class BrandRepository : OPCBaseRepository<int, Brand>, IBrandRepository
    {
        #region methods

        private static Expression<Func<Brand, bool>> Filter(BrandFilter filter)
        {
            var query = PredicateBuilder.True<Brand>();

            if (filter != null)
            {
                if (!String.IsNullOrWhiteSpace(filter.NamePrefix))
                    query = PredicateBuilder.And(query, v => v.Name.StartsWith(filter.NamePrefix) || v.EnglishName.StartsWith(filter.NamePrefix));

                if (!String.IsNullOrWhiteSpace(filter.Name))
                {
                    query = PredicateBuilder.And(query,
                        v => v.Name.Equals(filter.Name) || v.EnglishName.Equals(filter.Name));
                }

                if (filter.Status != null)
                {
                    query = PredicateBuilder.And(query, v => v.Status == filter.Status.Value);
                }
                else
                {
                    query = PredicateBuilder.And(query, v => v.Status > 0);
                }
            }

            return query;
        }

        private static Expression<Func<IMS_SectionBrand, bool>> Filter4SectionBrand(BrandFilter filter)
        {
            var query = PredicateBuilder.True<IMS_SectionBrand>();

            if (filter != null)
            {

                if (filter.CounterId != null)
                {

                    //return v => v.SectionId == filter.CounterId;
                    query = PredicateBuilder.And(query, v => v.SectionId == filter.CounterId);
                }


            }

            return query;
            //return v => true;
        }

        private static Expression<Func<Section, bool>> Filter4Section(BrandFilter filter)
        {
            var query = PredicateBuilder.True<Section>();

            if (filter != null)
            {

                if (filter.CounterId != null)
                {

                    //return v => v.SectionId == filter.CounterId;
                    query = PredicateBuilder.And(query, v => v.Id == filter.CounterId);
                }

                if (filter.Status != null)
                {
                    query = PredicateBuilder.And(query, v => v.Status == filter.Status.Value);
                }
                else
                {
                    query = PredicateBuilder.And(query, v => v.Status > 0);
                }
            }

            return query;
            //return v => true;
        }

        private static Func<IQueryable<Brand>, IOrderedQueryable<Brand>> OrderBy(BrandSortOrder sortOrder)
        {
            Func<IQueryable<Brand>, IOrderedQueryable<Brand>> orderBy = null;

            switch (sortOrder)
            {
                default:
                    //orderBy = v => v.OrderByDescending(s => s);
                    break;
            }

            return orderBy;
        }

        /// <summary>
        /// 关系处理
        /// </summary>
        /// <param name="c"></param>
        /// <param name="entity"></param>
        private void Relationship(DbContext c, Brand entity, List<int> Suppliers, List<int> Sections)
        {
            if (Suppliers != null)
            {
                var list =
                    Suppliers.Select(v => new Supplier_Brand() { Supplier_Id = v, Brand_Id = entity.Id });

                EFHelper.Delete<Supplier_Brand>(c, v => v.Brand_Id == entity.Id);
                c.Set<Supplier_Brand>().AddOrUpdate(v => new { v.Supplier_Id, v.Brand_Id }, list.ToArray());
                c.SaveChanges();

            }
            else
            {
                //删除 关系
                EFHelper.Delete<Supplier_Brand>(c, v => v.Brand_Id == entity.Id);
            }

            if (Sections != null)
            {
                var list =
                    Sections.Select(v => new IMS_SectionBrand() { SectionId = v, BrandId = entity.Id });

                EFHelper.Delete<IMS_SectionBrand>(c, v => v.BrandId == entity.Id);
                c.Set<IMS_SectionBrand>().AddOrUpdate(v => new { v.SectionId, v.BrandId }, list.ToArray());
                c.SaveChanges();
                //EFHelper.InsertOrUpdate(c, list.ToArray());
            }
            else
            {
                EFHelper.Delete<IMS_SectionBrand>(c, v => v.BrandId == entity.Id);
            }
        }

        #endregion

        protected override DbQuery<Brand> DbQuery(DbContext context)
        {
            //return context.Set<Section>();
            return context.Set<Brand>(); //.Include("Sections").Include("Suppliers");
        }

        public IList<Brand> GetAll()
        {

            return Select(t => t.Status == 1);
        }

        public IList<Brand> GetByIds(int[] brandIds)
        {
            return Select(t => t.Status == 1 && brandIds.Contains(t.Id));
        }

        public override Brand GetItem(int key)
        {
            return Func(c =>
            {
                var q = from s in c.Set<Brand>().Where(v => v.Id == key)
                        let sb_let = (from sb in c.Set<IMS_SectionBrand>()
                                      //join s in c.Set<Sse>()
                                      where s.Id == sb.BrandId
                                      select new
                                      {
                                          sb.SectionId
                                      })
                        select new
                        {
                            s,
                            Sectons = sb_let
                        };

                var q1 = from b in q
                         let sup = (from s in c.Set<OpcSupplierInfo>()
                                    join sb in c.Set<Supplier_Brand>() on s.Id equals sb.Supplier_Id
                                    where sb.Brand_Id == b.s.Id
                                    select new OpcSupplierInfoClone
                                    {
                                        Address = s.Address,
                                        Contact = s.Contact,
                                        Corporate = s.Corporate,
                                        Contract = s.Contract,
                                        CreatedDate = s.CreatedDate,
                                        CreatedUser = s.CreatedUser,
                                        FaxNo = s.FaxNo,
                                        Id = s.Id,
                                        Memo = s.Memo,
                                        Status = s.Status,
                                        StoreId = s.StoreId,
                                        SupplierName = s.SupplierName,
                                        SupplierNo = s.SupplierNo,
                                        TaxNo = s.TaxNo,
                                        Telephone = s.Telephone,
                                        UpdatedDate = s.UpdatedDate,
                                        UpdatedUser = s.UpdatedUser,

                                    })

                         let se = (from s in c.Set<Section>()
                                   join s_b in c.Set<IMS_SectionBrand>() on s.Id equals s_b.SectionId
                                   where (s_b.BrandId == b.s.Id)
                                   select new SectionClone
                                   {
                                       BrandId = s.BrandId,
                                       ChannelSectionId = s.ChannelSectionId,
                                       ContactPerson = s.ContactPerson,
                                       ContactPhone = s.ContactPhone,
                                       CreateDate = s.CreateDate,
                                       CreateUser = s.CreateUser,
                                       Id = s.Id,
                                       Location = s.Location,
                                       Name = s.Name,
                                       Status = s.Status,
                                       StoreCode = s.StoreCode,
                                       StoreId = s.StoreId,
                                       UpdateDate = s.UpdateDate,
                                       UpdateUser = s.UpdateUser,
                                       SectionCode = s.SectionCode
                                   })
                         select new BrandClone()
                         {
                             ChannelBrandId = b.s.ChannelBrandId,
                             CreatedDate = b.s.CreatedDate,
                             CreatedUser = b.s.CreatedUser,
                             Description = b.s.Description,
                             EnglishName = b.s.EnglishName,
                             Group = b.s.Group,
                             Id = b.s.Id,
                             Logo = b.s.Logo,
                             Name = b.s.Name,
                             Sections = se,
                             Status = b.s.Status,
                             Suppliers = sup,
                             UpdatedDate = b.s.UpdatedDate,
                             UpdatedUser = b.s.UpdatedUser,
                             WebSite = b.s.WebSite
                         };

                var rst = q1.FirstOrDefault();

                return AutoMapper.Mapper.Map<BrandClone, Brand>(rst);
            });
        }

        public override void Update(Brand entity)
        {

            Action(c =>
            {
                using (var trans = new TransactionScope())
                {

                    List<int> supplier_ids = null;
                    List<int> section_ids = null;
                    if (entity.Suppliers != null)
                    {
                        supplier_ids = entity.Suppliers.Where(v => v != null).Select(v => v.Id).ToList();
                        entity.Suppliers = null;
                    }

                    if (entity.Sections != null)
                    {
                        section_ids = entity.Sections.Select(v => v.Id).ToList();
                        entity.Sections = null;
                    }

                    EFHelper.Update(c, entity);

                    Relationship(c, entity, supplier_ids, section_ids);

                    trans.Complete();
                }
            });


        }

        public override Brand Insert(Brand entity)
        {
            //关系保存

            return Func(c =>
            {

                using (var trans = new TransactionScope())
                {
                    List<int> supplier_ids = null;
                    List<int> section_ids = null;
                    if (entity.Suppliers != null)
                    {
                        supplier_ids = entity.Suppliers.Where(v => v != null).Select(v => v.Id).ToList();
                        entity.Suppliers = null;
                    }

                    if (entity.Sections != null)
                    {
                        section_ids = entity.Sections.Select(v => v.Id).ToList();
                        entity.Sections = null;
                    }

                    var item = EFHelper.Insert(c, entity);
                    entity.Id = item.Id;


                    Relationship(c, entity, supplier_ids, section_ids);

                    trans.Complete();

                    return item;
                }
            });



        }

        /// <summary>
        ///  分页
        /// </summary>
        /// <param name="pagerRequest"></param>
        /// <param name="totalCount"></param>
        /// <param name="filter"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<Brand> GetPagedList(PagerRequest pagerRequest, out int totalCount, BrandFilter filter, BrandSortOrder sortOrder)
        {
            var brandFilter = Filter(filter);
            var fi = Filter4SectionBrand(filter);
            var sectionFilter = Filter4Section(filter);

            var result = Func(c =>
            {
                int t;

                var q = from s in c.Set<Brand>().AsExpandable().Where(brandFilter)
                        let sb_let = (from sb in c.Set<IMS_SectionBrand>().AsExpandable().Where(fi)
                                      //join s in c.Set<Sse>()
                                      where s.Id == sb.BrandId
                                      select new
                                      {
                                          sb.SectionId
                                      })
                        select new
                        {
                            s,
                            Sectons = sb_let
                        };

                t = q.Count();

                var q1 = from b in q.OrderBy(v => v.s.Id).Skip(pagerRequest.SkipCount).Take(pagerRequest.PageSize)
                         let sup = (from s in c.Set<OpcSupplierInfo>()
                                    join sb in c.Set<Supplier_Brand>() on s.Id equals sb.Supplier_Id
                                    where sb.Brand_Id == b.s.Id
                                    select new OpcSupplierInfoClone
                                    {
                                        Address = s.Address,
                                        Contact = s.Contact,
                                        Corporate = s.Corporate,
                                        Contract = s.Contract,
                                        CreatedDate = s.CreatedDate,
                                        CreatedUser = s.CreatedUser,
                                        FaxNo = s.FaxNo,
                                        Id = s.Id,
                                        Memo = s.Memo,
                                        Status = s.Status,
                                        StoreId = s.StoreId,
                                        SupplierName = s.SupplierName,
                                        SupplierNo = s.SupplierNo,
                                        TaxNo = s.TaxNo,
                                        Telephone = s.Telephone,
                                        UpdatedDate = s.UpdatedDate,
                                        UpdatedUser = s.UpdatedUser,

                                    })

                         let se = (from s in c.Set<Section>().AsExpandable().Where(sectionFilter)
                                   join s_b in c.Set<IMS_SectionBrand>().AsExpandable() on s.Id equals s_b.SectionId
                                   where (s_b.BrandId == b.s.Id)
                                   select new SectionClone
                                   {
                                       BrandId = s.BrandId,
                                       ChannelSectionId = s.ChannelSectionId,
                                       ContactPerson = s.ContactPerson,
                                       ContactPhone = s.ContactPhone,
                                       CreateDate = s.CreateDate,
                                       CreateUser = s.CreateUser,
                                       Id = s.Id,
                                       Location = s.Location,
                                       Name = s.Name,
                                       Status = s.Status,
                                       StoreCode = s.StoreCode,
                                       StoreId = s.StoreId,
                                       UpdateDate = s.UpdateDate,
                                       UpdateUser = s.UpdateUser,
                                       SectionCode = s.SectionCode
                                   })
                         select new BrandClone()
                         {
                             ChannelBrandId = b.s.ChannelBrandId,
                             CreatedDate = b.s.CreatedDate,
                             CreatedUser = b.s.CreatedUser,
                             Description = b.s.Description,
                             EnglishName = b.s.EnglishName,
                             Group = b.s.Group,
                             Id = b.s.Id,
                             Logo = b.s.Logo,
                             Name = b.s.Name,
                             Sections = se,
                             Status = b.s.Status,
                             Suppliers = sup,
                             UpdatedDate = b.s.UpdatedDate,
                             UpdatedUser = b.s.UpdatedUser,
                             WebSite = b.s.WebSite
                         };

                var rst = q1.ToList();

                return new
                {
                    totalCount = t,
                    Data = rst
                };
            });

            totalCount = result.totalCount;

            return AutoMapper.Mapper.Map<List<BrandClone>, List<Brand>>(result.Data);
        }

        public override IEnumerable<Brand> AutoComplete(string query)
        {
            var filter = Filter(new BrandFilter
            {
                NamePrefix = query
            });

            return Func(v => EFHelper.Get(DbQuery(v), filter, null, 20).ToList());
        }
    }
}