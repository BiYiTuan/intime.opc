// ***********************************************************************
// Assembly         : 01_Intime.OPC.Repository
// Author           : Liuyh
// Created          : 03-25-2014 01:21:46
//
// Last Modified By : Liuyh
// Last Modified On : 03-25-2014 01:22:26
// ***********************************************************************
// <copyright file="SectionRepository.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Enums.SortOrder;
using Intime.OPC.Domain.Models;
using Intime.OPC.Domain.Partials.Models;
using Intime.OPC.Repository.Base;
using LinqKit;
using PredicateBuilder = LinqKit.PredicateBuilder;

namespace Intime.OPC.Repository.Impl
{
    /// <summary>
    ///  专柜
    /// </summary>
    public class SectionRepository : OPCBaseRepository<int, Section>, ISectionRepository
    {
        #region methods

        /// <summary>
        /// section filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static Expression<Func<Section, bool>> Filter(SectionFilter filter)
        {
            var query = PredicateBuilder.True<Section>();

            if (filter != null)
            {
                if (!String.IsNullOrWhiteSpace(filter.NamePrefix))
                    query = PredicateBuilder.And(query, v => v.Name.StartsWith(filter.NamePrefix));

                if (filter.Status != null)
                {
                    query = PredicateBuilder.And(query, v => v.Status == filter.Status);
                }
                else
                {
                    query = PredicateBuilder.And(query, v => v.Status >= 0);
                }

                if (!String.IsNullOrWhiteSpace(filter.Name))
                {
                    query = PredicateBuilder.And(query, v => v.Name == filter.Name);
                }

                if (filter.StoreId != null)
                {
                    query = PredicateBuilder.And(query, v => v.StoreId == filter.StoreId);
                }

                if (filter.DataRoleStores != null && filter.StoreId == null)
                {
                    query = PredicateBuilder.And(query, v => filter.DataRoleStores.Contains(v.StoreId.HasValue ? v.StoreId.Value : -1));
                }

                if (!String.IsNullOrWhiteSpace(filter.SectionCode))
                {
                    query = PredicateBuilder.And(query, v => v.SectionCode == filter.SectionCode);
                }
            }

            return query;
        }

        /// <summary>
        /// section_brand filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static Expression<Func<IMS_SectionBrand, bool>> Filter4SectionBrand(SectionFilter filter)
        {
            var query = PredicateBuilder.True<IMS_SectionBrand>();

            if (filter != null)
            {
                if (filter.BrandId != null)
                {
                    query = PredicateBuilder.And(query, v => v.BrandId == filter.BrandId);
                }

            }

            return query;

        }

        /// <summary>
        /// 品牌表 filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static Expression<Func<Brand, bool>> Filter4Brand(SectionFilter filter)
        {
            var query = PredicateBuilder.True<Brand>();

            if (filter != null)
            {
                if (filter.Status != null)
                {
                    query = PredicateBuilder.And(query, v => v.Status == filter.Status.Value);
                }
                else
                {
                    query = PredicateBuilder.And(query, v => v.Status > 0);
                }

                if (filter.BrandId != null)
                {
                    query = PredicateBuilder.And(query, v => v.Id == filter.BrandId.Value);
                }

            }

            return query;
        }

        private static Func<IQueryable<Section>, IOrderedQueryable<Section>> OrderBy(SectionSortOrder sortOrder)
        {
            Func<IQueryable<Section>, IOrderedQueryable<Section>> orderBy = null;

            switch (sortOrder)
            {
                default:
                    orderBy = v => v.OrderByDescending(s => s.Id);
                    break;
            }

            return orderBy;
        }

        /// <summary>
        /// 关系处理
        /// </summary>
        /// <param name="c"></param>
        /// <param name="entity"></param>
        private void Relationship(DbContext c, Section entity, List<int> brandids)
        {
            EFHelper.Delete<IMS_SectionBrand>(c, v => v.SectionId == entity.Id);

            if (brandids != null)
            {
                var list =
                    brandids.Select(v => new IMS_SectionBrand() { SectionId = entity.Id, BrandId = v });

                c.Set<IMS_SectionBrand>().AddOrUpdate(v =>
                 new
                 {
                     v.BrandId,
                     v.SectionId
                 }
                , list.ToArray());

                c.SaveChanges();
                //EFHelper.InsertOrUpdate(c, list.ToArray());
            }
        }


        #endregion

        public override IEnumerable<Section> AutoComplete(string query)
        {
            var filter = Filter(new SectionFilter { NamePrefix = query });
            return Func(v => EFHelper.Get(DbQuery(v), filter, null).ToList());
        }

        public List<Section> GetPagedList(PagerRequest pagerRequest, out int totalCount, SectionFilter filter, SectionSortOrder sortOrder)
        {
            var sectionbrandFilter = Filter4SectionBrand(filter);
            var sectionFilter = Filter(filter);
            var brandFilter = Filter4Brand(filter);

            var result = Func(c =>
            {
                int t;

                var qt = from s in c.Set<Section>().AsExpandable().Where(sectionFilter)
                         join store in c.Set<Store>() on s.StoreId equals store.Id into tmp1
                         from store in tmp1.DefaultIfEmpty()

                         let s_b_let = (from sb in c.Set<IMS_SectionBrand>().AsExpandable().Where(sectionbrandFilter)
                                        where s.Id == sb.SectionId
                                        select new
                                        {
                                            sb.SectionId
                                        }
                                       )

                         select new
                         {
                             section = s,
                             sbs = s_b_let,
                             Store = store == null ? null : store
                         };

                t = qt.Count();

                var qr = from s in qt.OrderBy(v => v.section.Id).Skip(pagerRequest.SkipCount).Take(pagerRequest.PageSize)

                         let brands = (
                             from b in c.Set<Brand>().Where(brandFilter)
                             join s_b in c.Set<IMS_SectionBrand>() on b.Id equals s_b.BrandId
                             where s.section.Id == s_b.SectionId
                             select new BrandClone
                             {
                                 ChannelBrandId = b.ChannelBrandId,
                                 CreatedDate = b.CreatedDate,
                                 CreatedUser = b.CreatedUser,
                                 Description = b.Description,
                                 EnglishName = b.EnglishName,
                                 Group = b.Group,
                                 Id = b.Id,
                                 Logo = b.Logo,
                                 Name = b.Name,
                                 Status = b.Status,
                                 UpdatedDate = b.UpdatedDate,
                                 UpdatedUser = b.UpdatedUser,
                                 WebSite = b.WebSite,

                             }
                             )
                         select new SectionClone()
                         {
                             BrandId = s.section.BrandId,
                             ChannelSectionId = s.section.ChannelSectionId,
                             ContactPerson = s.section.ContactPerson,
                             ContactPhone = s.section.ContactPhone,
                             CreateDate = s.section.CreateDate,
                             CreateUser = s.section.CreateUser,
                             Id = s.section.Id,
                             Location = s.section.Location,
                             Name = s.section.Name,
                             Status = s.section.Status,
                             StoreCode = s.section.StoreCode,
                             StoreId = s.section.StoreId,
                             UpdateDate = s.section.UpdateDate,
                             UpdateUser = s.section.UpdateUser,
                             Brands = brands,
                             SectionCode = s.section.SectionCode,
                             Store = s.Store
                         };


                return new
                {
                    totalCount = t,
                    Data = qr.ToList()
                };
            });

            totalCount = result.totalCount;

            return AutoMapper.Mapper.Map<List<SectionClone>, List<Section>>(result.Data);
        }

        public override Section GetItem(int key)
        {
            return Func(c =>
            {
                var qt = from s in c.Set<Section>().AsExpandable().Where(v => v.Id == key)
                         join store in c.Set<Store>() on s.StoreId equals store.Id into tmp1
                         from store in tmp1.DefaultIfEmpty()
                         let s_b_let = (from sb in c.Set<IMS_SectionBrand>().AsExpandable()
                                        where s.Id == sb.SectionId
                                        select new
                                        {
                                            sb.SectionId
                                        }
                                       )

                         select new
                         {
                             section = s,
                             sbs = s_b_let,
                             Store = store == null ? null : store
                         };
                //var ww = qt.FirstOrDefault();

                //var qwww = ww;
                var qr = from s in qt.OrderBy(v => v.section.Id)
                         let brands = (
                             from b in c.Set<Brand>()
                             join s_b in c.Set<IMS_SectionBrand>() on b.Id equals s_b.BrandId
                             where s.section.Id == s_b.SectionId
                             select new BrandClone
                             {
                                 ChannelBrandId = b.ChannelBrandId,
                                 CreatedDate = b.CreatedDate,
                                 CreatedUser = b.CreatedUser,
                                 Description = b.Description,
                                 EnglishName = b.EnglishName,
                                 Group = b.Group,
                                 Id = b.Id,
                                 Logo = b.Logo,
                                 Name = b.Name,
                                 Status = b.Status,
                                 UpdatedDate = b.UpdatedDate,
                                 UpdatedUser = b.UpdatedUser,
                                 WebSite = b.WebSite
                             }
                             )
                         select new SectionClone()
                         {
                             BrandId = s.section.BrandId,
                             ChannelSectionId = s.section.ChannelSectionId,
                             ContactPerson = s.section.ContactPerson,
                             ContactPhone = s.section.ContactPhone,
                             CreateDate = s.section.CreateDate,
                             CreateUser = s.section.CreateUser,
                             Id = s.section.Id,
                             Location = s.section.Location,
                             Name = s.section.Name,
                             Status = s.section.Status,
                             StoreCode = s.section.StoreCode,
                             StoreId = s.section.StoreId,
                             UpdateDate = s.section.UpdateDate,
                             UpdateUser = s.section.UpdateUser,
                             Brands = brands,
                             SectionCode = s.section.SectionCode,
                             Store = s.Store// == null ? (StoreClone)null : new StoreClone()
                             //{
                             //    Id = s.Store.Id,
                             //    Name = s.Store.Name,
                             //    //ExStoreId = store.ExStoreId,
                             //    //GpsAlt = store.GpsAlt,
                             //    //GpsLng = store.GpsLng,
                             //    //GpsLat = store.GpsLat,
                             //    //Group_Id = store.Group_Id,
                             //    //Latitude = store.Latitude,
                             //    //Location = store.Location,
                             //    //Longitude = store.Longitude,
                             //    //Region_Id = store.Region_Id,
                             //    //RMAAddress = store.RMAAddress,
                             //    //RMAZipCode = store.RMAZipCode,
                             //    //RMAPerson = store.RMAPerson,
                             //    //RMAPhone = store.RMAPhone,
                             //    //Tel = store.Tel,
                             //    //StoreLevel = store.StoreLevel,

                             //    //CreatedDate = store.CreatedDate,
                             //    //CreatedUser = store.CreatedUser,
                             //    //Description = store.Description,
                             //    //Status = store.Status,
                             //    //UpdatedDate = store.UpdatedDate,
                             //    //UpdatedUser = store.CreatedUser

                             //}
                         };

                var ss = qr.FirstOrDefault();
                return AutoMapper.Mapper.Map<SectionClone, Section>(ss);
            });
        }

        public IList<Section> GetByStoreIDs(IList<int> storeIDs)
        {
            return Select(t => t.StoreId.HasValue && storeIDs.Contains(t.StoreId.Value));
        }

        public override Section Insert(Section entity)
        {
            return Func(c =>
            {
                using (var trans = new TransactionScope())
                {

                    List<int> brandids = null;
                    if (entity.Brands != null)
                    {
                        brandids = entity.Brands.Select(v => v.Id).ToList();
                        entity.Brands = null;
                    }

                    var item = EFHelper.Insert(c, entity);
                    entity.Id = item.Id;


                    Relationship(c, entity, brandids);

                    trans.Complete();
                    return item;
                }
            });
        }

        private readonly static List<string> UpdateFieldNames = new List<string>
            {
                        "Location",
        "ContactPhone",
        "Status",
        "BrandId",
        "StoreId",
        "UpdateDate",
        "UpdateUser",
        "ContactPerson",
        "Name",
        "StoreCode",
        "ChannelSectionId",
        "SectionCode"
            };

        public override void Update(Section entity)
        {
            Action(c =>
            {
                using (var trans = new TransactionScope())
                {
                    List<int> brandids = null;
                    if (entity.Brands != null)
                    {
                        brandids = entity.Brands.Select(v => v.Id).ToList();
                        entity.Brands = null;
                    }

                    EFHelper.UpdateEntityFields(c, entity, UpdateFieldNames);

                    Relationship(c, entity, brandids);
                    trans.Complete();
                }
            });
        }
    }
}