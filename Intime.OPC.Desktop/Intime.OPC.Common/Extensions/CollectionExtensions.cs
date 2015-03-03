// ***********************************************************************
// Assembly         : GasMap.Core
// Author           : liuyh
// Created          : 04-08-2013
//
// Last Modified By : liuyh
// Last Modified On : 04-08-2013
// ***********************************************************************
// <copyright file="CollectionExtensions.cs" company="Tecocity">
//     Copyright (c) Tecocity. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Intime.OPC.Common.Extensions
{
    /// <summary>
    ///     Collection的扩展类
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        ///     添加多个列表项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <param name="otherCol">The other col.</param>
        /// <returns>ICollection{``0}.</returns>
        public static void Add<T>(this ICollection<T> col, IEnumerable<T> otherCol)
        {
            foreach (T item in otherCol)
            {
                col.Add(item);
            }
        }

        /// <summary>
        ///     Fors the each.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T item in collection)
            {
                action(item);
            }
        }


        /// <summary>
        ///     比较返回序列中的某列的非重复值
        /// </summary>
        /// <typeparam name="TSource">序列</typeparam>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="source">序列</param>
        /// <param name="selector">列</param>
        /// <returns>IEnumerable{``1}.</returns>
        public static IEnumerable<TResult> Distinct<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TResult> selector)
        {
            IEnumerable<TResult> res = source.Select(selector);
            return res.Distinct();
        }
    }
}