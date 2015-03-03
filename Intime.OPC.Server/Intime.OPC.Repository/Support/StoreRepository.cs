// ***********************************************************************
// Assembly         : 01_Intime.OPC.Repository
// Author           : Liuyh
// Created          : 03-25-2014 01:20:45
//
// Last Modified By : Liuyh
// Last Modified On : 03-25-2014 01:21:19
// ***********************************************************************
// <copyright file="StoreRepository.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Enums.SortOrder;
using Intime.OPC.Domain.Extensions;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PredicateBuilder = LinqKit.PredicateBuilder;

namespace Intime.OPC.Repository.Support
{
    /// <summary>
    ///     Class StoreRepository.
    /// </summary>
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        private static readonly List<string> StdDefindFields = new List<string>
        {
            "Name",
      "Description",
      "Location",
      "Tel",
      //"CreatedUser",
      //"CreatedDate",
      "UpdatedDate",
      "UpdatedUser",
      //"Longitude",
      //"Latitude",
      "Group_Id",
      "Status",
      "Region_Id",
      "StoreLevel",
      //"GpsLat",
      //"GpsLng",
      //"GpsAlt",
      "ExStoreId",
      "RMAAddress",
      "RMAZipCode",
      "RMAPerson",
      "RMAPhone",
        };

        #region methods

        private static Expression<Func<Store, bool>> Filter(StoreFilter filter)
        {
            var query = PredicateBuilder.True<Store>();

            if (filter != null)
            {
                if (!String.IsNullOrWhiteSpace(filter.NamePrefix))
                    query = PredicateBuilder.And(query, v => v.Name.StartsWith(filter.NamePrefix));

                if (filter.Status != null)
                {
                    query = PredicateBuilder.And(query, v => v.Status == filter.Status.Value);
                }
                else
                {
                    query = PredicateBuilder.And(query, v => v.Status > 0);
                }

                if (filter.DataStoreIds != null)
                {
                    query = PredicateBuilder.And(query, v => filter.DataStoreIds.Contains(v.Id));
                }
            }

            return query;
        }

        private static Func<IQueryable<Store>, IOrderedQueryable<Store>> OrderBy(StoreSortOrder sortOrder)
        {
            Func<IQueryable<Store>, IOrderedQueryable<Store>> orderBy;

            switch (sortOrder)
            {
                default:
                    orderBy = v => v.OrderByDescending(s => s.StoreLevel);
                    break;
            }

            return orderBy;
        }

        private static void CheckEntity(Store entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entity.Description = entity.Description.NullToEmpty();
            entity.Name = entity.Name.NullToEmpty();
            entity.Location = entity.Location.NullToEmpty();
            entity.Tel = entity.Tel.NullToEmpty();
        }

        #endregion

        public new PageResult<Store> GetAll(int pageIndex, int pageSize = 20)
        {
            return Select(t => t.Status > 0, t => t.UpdatedDate, false, pageIndex, pageSize);
        }

        public List<Store> GetPagedList(PagerRequest request, out int totalCount, StoreFilter filter)
        {

            var query = Filter(filter);
            var order = OrderBy(filter.SortOrder ?? StoreSortOrder.Default);

            using (var db = new YintaiHZhouContext())
            {
                int t;
                var rst = EFHelper.GetPaged(db, query, out t, request.PageIndex, request.PageSize, order).ToList();
                totalCount = t;
                return rst;
            }
        }

        public Store GetItem(int id)
        {
            using (var db = new YintaiHZhouContext())
            {
                return EFHelper.FindOne<Store>(db, v => v.Id == id);
            }
        }

        public Store Insert(Store entity)
        {
            CheckEntity(entity);

            using (var db = new YintaiHZhouContext())
            {
                var rst = EFHelper.Insert(db, entity);

                return rst;
            }
        }

        public new bool Update(Store entity)
        {
            CheckEntity(entity);
            using (var db = new YintaiHZhouContext())
            {
                EFHelper.UpdateEntityFields(db, entity, StdDefindFields);
            }

            return true;
        }
    }
}