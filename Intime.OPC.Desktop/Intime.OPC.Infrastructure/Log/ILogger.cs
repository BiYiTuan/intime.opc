// ***********************************************************************
// Assembly         : GasMap.Core
// Author           : liuyh
// Created          : 03-04-2013
//
// Last Modified By : liuyh
// Last Modified On : 03-21-2013
// ***********************************************************************
// <copyright file="ILogger.cs" company="">
//     Copyright (c)  All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Intime.OPC.Infrastructure
{
    /// <summary>
    ///     日志操作
    ///     test
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        ///     写日志 日志级别为Debug
        /// </summary>
        /// <param name="message">消息</param>
        void Debug(string message);

        /// <summary>
        ///     写日志 日志级别为Debug
        /// </summary>
        /// <param name="format">消息格式字符串</param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象</param>
        void Debug(string format, params object[] args);

        /// <summary>
        ///     写日志 日志级别为 Error
        /// </summary>
        /// <param name="message">消息</param>
        void Error(string message);

        /// <summary>
        ///     写日志 日志级别为 Error
        /// </summary>
        /// <param name="format">消息格式字符串</param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象</param>
        void Error(string format, params object[] args);

        /// <summary>
        ///     写日志 日志级别为 Error
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="t">系统错误类</param>
        void Error(string message, Exception t);

        /// <summary>
        ///     写日志 日志级别为 Info
        /// </summary>
        /// <param name="message">消息</param>
        void Info(string message);

        /// <summary>
        ///     写日志 日志级别为 Info
        /// </summary>
        /// <param name="format">消息格式字符串</param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象</param>
        void Info(string format, params object[] args);

        /// <summary>
        ///     写日志 日志级别为 Warning
        /// </summary>
        /// <param name="message">消息</param>
        void Warning(string message);

        /// <summary>
        ///     写日志 日志级别为 Warning
        /// </summary>
        /// <param name="format">消息格式字符串</param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象</param>
        void Warning(string format, params object[] args);
    }

    /// <summary>
    ///     日志工厂
    /// </summary>
    public class LogFactory
    {
        /// <summary>
        ///     获得一个日志操作类
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>日志操作类</returns>
        public static ILogger GetLogger(Type type)
        {
            return new Log4NetLogger(type);
        }
    }
}