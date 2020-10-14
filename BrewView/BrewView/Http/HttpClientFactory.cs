using System;
using System.Net.Http;
using System.Net.Http.Headers;
using BrewView.Services.Account;

namespace BrewView.Http
{
    public class HttpClientFactory : IHttpClientFactory
    {
        private readonly HttpClient m_restClient;
        private readonly HttpClient m_graphQLClient;

        public HttpClientFactory()
        {
            m_restClient = new HttpClient(new HttpClientHandler
                {
#if DEBUG
                    ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
#endif
                });

            m_graphQLClient = new HttpClient(new HttpClientHandler
            {
#if DEBUG
                ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
#endif
            });
        }

        public HttpClient CreateRestClient()
        {
            m_restClient.BaseAddress = new Uri(AppConstants.BaseAddress);
            return m_restClient;
        }


        public HttpClient CreateGraphQLClient()
        {
            m_graphQLClient.BaseAddress = new Uri(AppConstants.BaseAddress);
            return m_graphQLClient;
        }
    }
}