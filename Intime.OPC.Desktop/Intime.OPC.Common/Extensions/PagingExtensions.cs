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

using System.Collections.Generic;

namespace System.Linq
{
    /// <summary>
    ///     Class PagingExtensions
    /// </summary>
    public static class PagingExtensions
    {
        #region Methods

        /// <summary>
        ///     ��ҳ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageNumber">ҳ��</param>
        /// <param name="pageSize">ҳ��С</param>
        /// <returns>��ҳ��Ľ��</returns>
        public static IQueryable<T> Page<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            return query.Skip((pageNumber*pageSize)).Take(pageSize);
        }

        /// <summary>
        ///     ��ҳ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="pageNumber">ҳ��</param>
        /// <param name="pageSize">ҳ��С</param>
        /// <returns>��ҳ��Ľ��</returns>
        public static IEnumerable<T> Page<T>(this IEnumerable<T> query, int pageNumber, int pageSize)
        {
            return query.Skip(((pageNumber - 1)*pageSize)).Take(pageSize);
        }

        #endregion Methods
    }
}