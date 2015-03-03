// ***********************************************************************
// Assembly         : Intime.OPC.Desktop.Infrastructure
// Author           : Liuyh
// Created          : 03-15-2014 15:01:02
//
// Last Modified By : Liuyh
// Last Modified On : 03-15-2014 15:09:58
// ***********************************************************************
// <copyright file="Mapper.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using AutoMapper;

namespace Intime.OPC.Infrastructure
{
    /// <summary>
    ///     Class Mapper.
    /// </summary>
    public class Mapper
    {
        /// <summary>
        ///     The regist collection
        /// </summary>
        private static readonly ICollection<string> registCollection = new Collection<string>();

        /// <summary>
        ///     两个类型之间的映射
        /// </summary>
        /// <typeparam name="TSource">The type of the t source.</typeparam>
        /// <typeparam name="TTagart">The type of the t tagart.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>``1.</returns>
        public static TTagart Map<TSource, TTagart>(TSource source, Expression<Func<TSource, TTagart>> convert = null)
        {
            string key = typeof (TSource).FullName + "_" + typeof (TTagart).FullName;
            if (!registCollection.Contains(key))
            {
                IMappingExpression<TSource, TTagart> map = AutoMapper.Mapper.CreateMap<TSource, TTagart>();
                if (convert != null)
                {
                    map.ConvertUsing(convert.Compile());
                }
                registCollection.Add(key);
            }

            return AutoMapper.Mapper.Map<TSource, TTagart>(source);
        }

        public static IList<TTagart> Map<TSource, TTagart>(IEnumerable<TSource> source,
            Expression<Func<TSource, TTagart>> convert = null)
        {
            string key = typeof (TSource).FullName + "_" + typeof (TTagart).FullName;
            if (!registCollection.Contains(key))
            {
                IMappingExpression<TSource, TTagart> map = AutoMapper.Mapper.CreateMap<TSource, TTagart>();
                if (convert != null)
                {
                    map.ConvertUsing(convert.Compile());
                }
                registCollection.Add(key);
            }
            IList<TTagart> lst = new List<TTagart>();
            foreach (TSource s in source)
            {
                lst.Add(AutoMapper.Mapper.Map<TSource, TTagart>(s));
            }
            return lst;
        }

        public static void CreateMap<TSource, TTagart>(Expression<Func<TSource, TTagart>> convert = null)
        {
            string key = typeof (TSource).FullName + "_" + typeof (TTagart).FullName;
            if (!registCollection.Contains(key))
            {
                IMappingExpression<TSource, TTagart> map = AutoMapper.Mapper.CreateMap<TSource, TTagart>();
                if (convert != null)
                {
                    map.ConvertUsing(convert.Compile());
                }
                registCollection.Add(key);
            }
        }
    }
}