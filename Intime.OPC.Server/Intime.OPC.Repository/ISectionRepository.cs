// ***********************************************************************
// Assembly         : 01_Intime.OPC.Repository
// Author           : Liuyh
// Created          : 03-25-2014 01:14:33
//
// Last Modified By : Liuyh
// Last Modified On : 03-25-2014 01:14:48
// ***********************************************************************
// <copyright file="ISectionRepository.cs" company="">
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
    /// 专柜
    /// </summary>
    public interface ISectionRepository : IOPCRepository<int, Section>, IRepository<Section>
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pagerRequest">分页请求参数</param>
        /// <param name="totalCount">记录总数</param>
        /// <param name="filter">筛选项</param>
        /// <param name="sortOrder">排序项</param>
        /// <returns></returns>
        List<Section> GetPagedList(PagerRequest pagerRequest, out int totalCount, SectionFilter filter,
                                   SectionSortOrder sortOrder);


        IList<Section> GetByStoreIDs(IList<int> storeIDs);
    }
}