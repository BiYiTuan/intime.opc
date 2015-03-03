// ***********************************************************************
// Assembly         : Intime.OPC.Infrastructure
// Author           : liuyh
// Created          : 03-13-2014 22:24:36
//
// Last Modified By : liuyh
// Last Modified On : 03-13-2014 22:28:02
// ***********************************************************************
// <copyright file="Log4NetLogger.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using log4net;
using log4net.Config;

namespace Intime.OPC.Infrastructure
{
    /// <summary>
    ///     Class Log4NetLogger.
    /// </summary>
    public class Log4NetLogger : ILogger
    {
        /// <summary>
        ///     The log
        /// </summary>
        private readonly ILog Log;

        #region Implementation of ILogger

        /// <summary>
        ///     写日志 日志级别为 Warning
        /// </summary>
        /// <param name="message">消息</param>
        public void Warning(string message)
        {
            Log.Warn(message);
        }

        /// <summary>
        ///     写日志 日志级别为Debug
        /// </summary>
        /// <param name="message">消息</param>
        public void Debug(string message)
        {
            Log.Debug(message);
        }

        /// <summary>
        ///     写日志 日志级别为 Info
        /// </summary>
        /// <param name="message">消息</param>
        public void Info(string message)
        {
            Log.Info(message);
        }

        /// <summary>
        ///     写日志 日志级别为 Error
        /// </summary>
        /// <param name="message">消息</param>
        public void Error(string message)
        {
            Log.Error(message);
        }

        /// <summary>
        ///     写日志 日志级别为 Error
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="t">系统错误类</param>
        public void Error(string message, Exception t)
        {
            Log.Error(message);
            Log.ErrorFormat("Error: {0}, exception: {1}", t.Message, t);
        }

        /// <summary>
        ///     写日志 日志级别为Debug
        /// </summary>
        /// <param name="format">消息格式字符串</param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象</param>
        public void Debug(string format, params object[] args)
        {
            Log.DebugFormat(format, args);
        }

        /// <summary>
        ///     写日志 日志级别为 Error
        /// </summary>
        /// <param name="format">消息格式字符串</param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象</param>
        public void Error(string format, params object[] args)
        {
            Log.ErrorFormat(format, args);
        }

        /// <summary>
        ///     写日志 日志级别为 Info
        /// </summary>
        /// <param name="format">消息格式字符串</param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象</param>
        public void Info(string format, params object[] args)
        {
            Log.InfoFormat(format, args);
        }

        /// <summary>
        ///     写日志 日志级别为 Warning
        /// </summary>
        /// <param name="format">消息格式字符串</param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象</param>
        public void Warning(string format, params object[] args)
        {
            Log.WarnFormat(format, args);
        }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="Log4NetLogger" /> class.
        /// </summary>
        internal Log4NetLogger()
            : this(typeof (ILogger))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Log4NetLogger" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        internal Log4NetLogger(Type type)
        {
            XmlConfigurator.Configure();
            Log = LogManager.GetLogger(type);
        }
    }
}