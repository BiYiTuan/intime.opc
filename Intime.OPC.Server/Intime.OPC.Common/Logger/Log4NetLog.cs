using log4net;
using log4net.Config;

[assembly: XmlConfigurator(ConfigFile = @"Config\Log4net.config", Watch = true)]

namespace Intime.OPC.Common.Logger
{
    public class Log4NetLog : ILog
    {
        private static readonly log4net.ILog _error = LogManager.GetLogger("ExceptionLogger");
        private static readonly log4net.ILog _info = LogManager.GetLogger("InfoLogger");
        private static readonly log4net.ILog _warn = LogManager.GetLogger("WarnLogger");
        private static readonly log4net.ILog _debug = LogManager.GetLogger("DebugLogger");

        #region Implementation of ILog

        /// <summary>
        ///     信息
        /// </summary>
        /// <param name="obj"></param>
        public void Info(object obj)
        {
            _info.Info(obj);
        }

        /// <summary>
        ///     异常
        /// </summary>
        /// <param name="obj"></param>
        public void Exception(object obj)
        {
            Error(obj);
        }

        /// <summary>
        ///     调试信息
        /// </summary>
        /// <param name="obj"></param>
        public void Debug(object obj)
        {
            _debug.Debug(obj);
        }

        /// <summary>
        ///     警告
        /// </summary>
        /// <param name="obj"></param>
        public void Warn(object obj)
        {
            _warn.Warn(obj);
        }

        /// <summary>
        ///     错误
        /// </summary>
        /// <param name="obj"></param>
        public void Error(object obj)
        {
            _error.Error(obj);
        }

        #endregion
    }
}