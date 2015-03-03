// ***********************************************************************
// Assembly         : GasMap.Core
// Author           : liuyh
// Created          : 03-26-2013
//
// Last Modified By : liuyh
// Last Modified On : 03-29-2013
// ***********************************************************************
// <copyright file="PagingExtensions.cs" company="Tecocity">
//     Copyright (c) Tecocity. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace System.Linq
{
    using Collections.Generic;


    /// <summary>
    /// Class PagingExtensions
    /// </summary>
    public static class PagingExtensions
    {
        #region Methods

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>分页后的结果</returns>
        public static IQueryable<T> Page<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex - 1;
            if (pageIndex<0)
            {
                pageIndex = 0;
            }
            return query.Skip((pageIndex * pageSize)).Take(pageSize);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="pageIndex">页码(从1开始)</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>分页后的结果</returns>
        public static IEnumerable<T> Page<T>(this IEnumerable<T> query, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex - 1;
            if (pageIndex < 0)
            {
                pageIndex = 0;
            }
            if (pageIndex==0)
            {
                return query.Take(pageSize);
            }
            return query.Skip((pageIndex * pageSize)).Take(pageSize);
        }

        #endregion Methods
    }
}