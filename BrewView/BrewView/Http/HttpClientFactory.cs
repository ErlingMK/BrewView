using System;
using System.Net.Http;

namespace BrewView.Http
{
    public class HttpClientFactory : IHttpClientFactory
    {
        private readonly HttpClient m_client;

        public HttpClientFactory()
        {
            m_client = new HttpClient(new HttpClientHandler
                {
#if DEBUG
                    ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
#endif
                });
        }

        public HttpClient CreateClient()
        {
            return m_client;
        }
    }
}