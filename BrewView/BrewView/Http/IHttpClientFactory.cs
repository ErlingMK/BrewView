using System.Net.Http;
using BrewView.Services.Account;

namespace BrewView.Http
{
    public interface IHttpClientFactory
    {
        HttpClient CreateRestClient();
        HttpClient CreateGraphQLClient();
    }
}