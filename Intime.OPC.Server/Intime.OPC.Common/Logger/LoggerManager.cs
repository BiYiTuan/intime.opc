namespace Intime.OPC.Common.Logger
{
    public class LoggerManager
    {
        private static readonly ILog Log;

        static LoggerManager()
        {
            Log = new Log4NetLog();
        }

        public static ILog Current()
        {
            return Log;
        }
    }
}