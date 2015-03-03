using System.Security.Cryptography.X509Certificates;

namespace Intime.OPC.Infrastructure.Config
{
    /// <summary>
    ///     Interface IConfig
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// 获得配置文件的值
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="defaultValue">默认值，当没有配置的时候，返回该值</param>
        /// <returns>System.String.</returns>
        string GetValue(string key, string defaultValue = "");

        string Password { get; set; }
        string UserKey { get; set; }
        string ServiceUrl { get; set; }
        string Version { get; set; }
    }
}