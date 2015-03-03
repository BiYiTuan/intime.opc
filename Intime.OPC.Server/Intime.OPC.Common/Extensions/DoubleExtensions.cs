// ***********************************************************************
// Assembly         : GasMap.Core
// Author           : liuyh
// Created          : 04-22-2013
//
// Last Modified By : liuyh
// Last Modified On : 04-22-2013
// ***********************************************************************
// <copyright file="DoubleExtensions.cs" company="Tecocity">
//     Copyright (c) Tecocity. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// Class DoubleExtensions
    /// </summary>
    public static   class DoubleExtensions
    {
        /// <summary>
        /// 由弧度转换为角度
        /// </summary>
        /// <param name="radian">弧度值</param>
        /// <returns>角度格式为dd°mm′ss″.</returns>
        public static string ToAngle(this double radian) {
            double d = radian * 180 / Math.PI;
            int dd = (int)Math.Floor(d);
            //计算分
            d = (d - dd) * 60;
            int mm = (int)Math.Floor(d);
            //计算秒
            d = (d - mm) * 60;
            int ss = (int)Math.Floor(d);

            return string.Format("{0}°{1}′{2}″", dd, mm, ss);
        }
    }
}
