// ***********************************************************************
// Assembly         : GasMap.Core
// Author           : liuyh
// Created          : 03-21-2013
//
// Last Modified By : Liuyh
// Last Modified On : 03-21-2013
// ***********************************************************************
// <copyright file="PageResult.cs" company="">
//     Copyright (c) Liuyh. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace Intime.OPC.Infrastructure
{
    /// <summary>
    ///     Class PageResult
    /// </summary>
    /// <typeparam name="TEntity">The type of the T entity.</typeparam>
    public class PageResult<TEntity>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PageResult{TEntity}" /> class.
        /// </summary>
        /// <param name="entites">提取的分页数据</param>
        /// <param name="totalCount">实体的总数</param>
        public PageResult(IList<TEntity> entites, int totalCount)
        {
            Result = entites;
            TotalCount = totalCount;
        }

        public PageResult()
        {
            Result = new List<TEntity>();
        }

        #endregion Constructors

        #region Properties

        private IList<TEntity> _result;

        /// <summary>
        ///     结果总数
        /// </summary>
        /// <value>The total count.</value>
        private int _totalCount;

        /// <summary>
        ///     分页数据
        /// </summary>
        /// <value>The result.</value>
        public IList<TEntity> Result { get; set; }

        public int TotalCount { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        ///     计算页数
        /// </summary>
        /// <param name="pageSize">页大小</param>
        /// <returns>System.Int32.</returns>
        public int TotalPages(int pageSize)
        {
            return (int) Math.Ceiling(Convert.ToDouble(TotalCount)/pageSize);
        }

        #endregion Methods
    }
}