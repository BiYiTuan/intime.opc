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
using System.Text;
using System.Collections;
using System.Linq.Expressions;

namespace System.Collections
{
    /// <summary>
    /// Collection的扩展类
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// 添加多个列表项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <param name="otherCol">The other col.</param>
        /// <returns>ICollection{``0}.</returns>
        public static void Add<T>(this ICollection<T> col, IEnumerable<T> otherCol)
        {
            foreach (var item in otherCol)
            {
                col.Add(item);
            }
        }

        /// <summary>
        /// Fors the each.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }


        /// <summary>
        /// 比较返回序列中的某列的非重复值
        /// </summary>
        /// <typeparam name="TSource">序列</typeparam>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="source">序列</param>
        /// <param name="selector">列</param>
        /// <returns>IEnumerable{``1}.</returns>
        public static IEnumerable<TResult> Distinct<TSource,TResult>(this IEnumerable<TSource> source, Func<TSource,TResult> selector){
            var res = source.Select<TSource, TResult>(selector);
            return res.Distinct();
        }

    }
}

namespace System.Collections.Generic
{
    /// <summary>
    /// Collection的扩展类
    /// </summary>
    public static class ListExtensions
    {

        /// <summary>
        /// 插入一个项,并确定在列表中唯一
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="index">The index.</param>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> list中不包含该项,则插入 <c>false</c>list中已经存在该项</returns>
        public static bool InsertUnique<T>(this IList<T> list, int index, T item)
        {
            if (list.Contains(item) == false)
            {
                list.Insert(index, item);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获得符合对比条件的列表中某项的序号
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "list">The list.</param>
        /// <param name = "comparison">The comparison.</param>
        /// <returns>The item index</returns>
        public static int IndexOf<T>(this IList<T> list, Func<T, bool> comparison)
        {
         
            for (var i = 0; i < list.Count; i++)
            {
                if (comparison(list[i]))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Fors the each.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }


        /// <summary>
        /// 合并两个数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list1">The list1.</param>
        /// <param name="list2">The list2.</param>
        /// <returns>List{``0}.</returns>
        public static List<T> Merge<T>(this List<T> list1, List<T> list2)
        {
            if (list1 != null && list2 != null) foreach (var item in list2.Where(item => !list1.Contains(item))) list1.Add(item);
            return list1;
        }

        /// <summary>
        /// 合并两个数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list1">The list1.</param>
        /// <param name="list2">The list2.</param>
        /// <param name="match">符合的条件</param>
        /// <returns>List{``0}.</returns>
        public static List<T> Merge<T>(this List<T> list1, List<T> list2, Expression<Func<T, object>> match)
        {
            if (list1 != null && list2 != null && match != null)
            {
                var matchFunc = match.Compile();
                foreach (var item in list2)
                {
                    var key = matchFunc(item);
                    if (!list1.Exists(i => matchFunc(i).Equals(key))) list1.Add(item);
                }
            }

            return list1;
        }


        /// <summary>
        /// 比较返回序列中的某列的非重复值
        /// </summary>
        /// <typeparam name="TSource">序列</typeparam>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="source">序列</param>
        /// <param name="selector">列</param>
        /// <returns>IEnumerable{``1}.</returns>
        public static IEnumerable<TResult> Distinct<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            var res = source.Select<TSource, TResult>(selector);
            return res.Distinct();
        }

        /// <summary>
        /// 是否包含符合條件的項
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="match">The match.</param>
        /// <returns><c>true</c> if [contains] [the specified source]; otherwise, <c>false</c>.</returns>
        public static bool Contains<T>(this IEnumerable<T> source, Expression<Func<T, bool>> match) {
            var t=  source.FirstOrDefault(match.Compile());
            return t != null;
        }

        /// <summary>
        /// 根据条件移除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="match">The match.</param>
        public static void Remove<T>(this IList<T> source, Expression<Func<T, bool>> match)
        {
            var lst = source.Where(match.Compile());
           lst.ForEach((t) =>
           {
               source.Remove(t);
           });
        }

        /// <summary>
        /// 将数组合并为字符串
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="split">The split.</param>
        /// <returns>System.String.</returns>
        public static string Join(this IEnumerable<int> source, char split=',')
        {
           
            StringBuilder stringBuilder=new StringBuilder();
        
            foreach (var id in source)
            {
                stringBuilder.AppendFormat("{0}{1}", id, split);
            }
            if (source.Count()>0)
            {
                return stringBuilder.ToString().TrimEnd(split);
            }
            return "";
        }
    }

    public class EqualityComparer<T> : IEqualityComparer<T>
    {
        public  EqualityComparer(Expression<Func<T,T, bool>> funcEquals) {
            _FuncEquals = funcEquals.Compile();
        }

        Func<T,T, bool> _FuncEquals;

        public bool Equals(T x, T y)
        {
            return _FuncEquals(x, y);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
    
}
