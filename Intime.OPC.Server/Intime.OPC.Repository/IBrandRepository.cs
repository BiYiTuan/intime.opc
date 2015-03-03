// ***********************************************************************
// Assembly         : 01_Intime.OPC.Repository
// Author           : Liuyh
// Created          : 03-28-2014 21:07:30
//
// Last Modified By : Liuyh
// Last Modified On : 03-28-2014 21:07:45
// ***********************************************************************
// <copyright file="IBrandRepository.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Enums.SortOrder;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository
{
    /// <summary>
    /// Interface IBrandRepository
    /// </summary>
    public interface IBrandRepository : IOPCRepository<int, Brand>, IRepository<Brand>
    {
        IList<Brand> GetAll();

        /// <summary>
        /// 获得多个品牌信息
        /// </summary>
        /// <param name="brandIds">The brand ids.</param>
        /// <returns>IList{Brand}.</returns>
        IList<Brand> GetByIds(int[] brandIds);

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pagerRequest">分页请求参数</param>
        /// <param name="totalCount">记录总数</param>
        /// <param name="filter">筛选项</param>
        /// <param name="sortOrder">排序项</param>
        /// <returns></returns>
        List<Brand> GetPagedList(PagerRequest pagerRequest, out int totalCount, BrandFilter filter,
                                   BrandSortOrder sortOrder);
    }
}