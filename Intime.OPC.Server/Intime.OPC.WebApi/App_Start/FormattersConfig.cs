using System.Web.Http;

namespace Intime.OPC.WebApi.App_Start
{
    public class FormattersConfig
    {
        public static void Config()
        {
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;

            GlobalConfiguration.Configuration.Formatters.Remove(
                GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            //json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }
    }
}