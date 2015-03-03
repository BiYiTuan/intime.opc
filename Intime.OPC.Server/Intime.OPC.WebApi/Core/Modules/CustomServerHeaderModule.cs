using System;
using System.Web;

namespace Intime.OPC.WebApi.Core.Modules
{
    public class CustomServerHeaderModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += OnPreSendRequestHeaders;
        }

        public void Dispose()
        { }

        static void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            // modify the "Server" Http Header
            HttpContext.Current.Response.Headers.Set("Server", "Intime's Webserver");
        }
    } 
}
