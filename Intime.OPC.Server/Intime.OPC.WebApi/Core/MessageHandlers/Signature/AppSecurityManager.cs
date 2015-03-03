using System;
using System.Configuration;

namespace Intime.OPC.WebApi.Core.MessageHandlers.Signature
{
    public class AppSecurityManager : IAppSecurityManager
    {
        #region IAppSecurityManager Members

        public string GetSecretKey(string appKey)
        {
            if (string.IsNullOrEmpty(appKey))
                return String.Empty;

            string configKey = string.Format("AppSecurityManager.{0}:SecretKey", appKey);

            string key = ConfigurationManager.AppSettings[configKey];

            return string.IsNullOrEmpty(key) ? string.Empty : key;
        }

        public bool Enabled
        {
            get
            {
                string enable = ConfigurationManager.AppSettings["AppSecurityManager:Enabled"];

                if (enable == null)
                {
                    return true;
                }

                bool result;

                bool.TryParse(enable, out result);

                return result;
            }
        }

        #endregion
    }
}