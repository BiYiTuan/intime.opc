using System.ComponentModel.Composition;

namespace Intime.OPC.Infrastructure.REST
{
    [Export(typeof(IRestClientFactory))]
    public class RestClientFactory : IRestClientFactory
    {
        public IRestClient Create(string baseAddress, string privateKey, string from, string token)
        {
            return new RestClient(baseAddress, privateKey, from, token);
        }
    }
}
