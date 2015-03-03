using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain
{
    public class ConfigManager
    {
        public static string IMS_DEFAULT_LOGO
        {
            get
            {
                return ConfigurationManager.AppSettings["IMS_Default_LOGO"];
            }
        }

        public static int IMS_DEFAULT_TEMPLATE
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["IMS_Default_Template"]);
            }
        }

        public static int IMS_INVITE_AUTHRIGHT
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["IMS_Invite_AuthRight"]);
            }
        }
    }
}
