// ***********************************************************************
// Assembly         : OPCApp.Main.Infrastructure
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
using Intime.OPC.Domain;

namespace Intime.OPC.Service.Map
{
    /// <summary>
    ///     Class Mapper.
    /// </summary>
    public  class Mapper
    {

        /// <summary>
        ///     两个类型之间的映射
        /// </summary>
        /// <typeparam name="TSource">The type of the t source.</typeparam>
        /// <typeparam name="TTagart">The type of the t tagart.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>``1.</returns>
        public static TTagart Map<TSource, TTagart>(TSource source, Expression<Func<TSource, TTagart>> convert = null)
        {
    
            return AutoMapper.Mapper.Map<TSource, TTagart>(source);
        }

        public static IList<TTagart> Map<TSource, TTagart>(IList<TSource> source)
        {
            return AutoMapper.Mapper.Map<IList<TSource>, IList<TTagart>>(source);
        }
        public static PageResult<TTagart> Map<TSource, TTagart>(PageResult<TSource> source)
        {
            var lst= AutoMapper.Mapper.Map<IList<TSource>, IList<TTagart>>(source.Result);
            return new PageResult<TTagart>(lst,source.TotalCount);
        }
        
    }
}