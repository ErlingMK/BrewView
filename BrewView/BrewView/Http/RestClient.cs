using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BrewView.Http
{
    public class RestClient : IRestClient
    {
        private readonly HttpClient m_client;

        public RestClient(IHttpClientFactory httpClientFactory)
        {
            m_client = httpClientFactory.CreateRestClient();
        }

        public async Task<T> Send<T>(HttpRequestMessage message)
        {
            var response = await m_client.SendAsync(message);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
                   