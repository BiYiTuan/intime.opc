// ***********************************************************************
// Assembly         : 01_Intime.OPC.Repository
// Author           : Liuyh
// Created          : 03-25-2014 01:13:16
//
// Last Modified By : Liuyh
// Last Modified On : 03-25-2014 01:13:43
// ***********************************************************************
// <copyright file="IStoreRepository.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Models;
using System.Collections.Generic;

namespace Intime.OPC.Repository
{
    /// <summary>
    ///     Interface IStoreRepository
    /// </summary>
    public interface IStoreRepository : IRepository<Store>
    {
        //PageResult<Store> GetAll(int pageIndex, int pageSize = 20);

        List<Store> GetPagedList(PagerRequest request, out int totalCount, StoreFilter filter);

        Store GetItem(int id);

        Store Insert(Store entity);
    }
}