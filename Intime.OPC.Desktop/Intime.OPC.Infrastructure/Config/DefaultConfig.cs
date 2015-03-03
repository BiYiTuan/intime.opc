using System.Configuration;
using System.Linq;

namespace Intime.OPC.Infrastructure.Config
{
    internal class DefaultConfig : IConfig
    {
        public DefaultConfig()
        {
             Password = ConfigurationManager.AppSettings["consumerSecret"];
             UserKey = ConfigurationManager.AppSettings["consumerKey"];
             ServiceUrl = ConfigurationManager.AppSettings["apiAddress"];
             Version = ConfigurationManager.AppSettings["version"];  
        }

        public string GetValue(string key, string defaultValue = "")
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
            {
                return ConfigurationManager.AppSettings[key];
            }
            return defaultValue;
        }

        public string Password { get; set; }
        public string UserKey { get; set; }
        public string ServiceUrl { get; set; }
        public string Version { get; set; }

        public string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}