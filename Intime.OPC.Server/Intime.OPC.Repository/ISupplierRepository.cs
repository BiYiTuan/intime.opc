﻿using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Enums.SortOrder;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository
{
    public interface ISupplierRepository : IOPCRepository<int, OpcSupplierInfo>
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pagerRequest">分页请求参数</param>
        /// <param name="totalCount">记录总数</param>
        /// <param name="filter">筛选项</param>
        /// <param name="sortOrder">排序项</param>
        /// <returns></returns>
        List<OpcSupplierInfo> GetPagedList(PagerRequest pagerRequest, out int totalCount, SupplierFilter filter,
                                   SupplierSortOrder sortOrder);
    }
}
