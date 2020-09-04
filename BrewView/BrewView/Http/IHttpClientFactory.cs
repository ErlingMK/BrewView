using System.Net.Http;

namespace BrewView.Http
{
    public interface IHttpClientFactory
    {
        HttpClient CreateClient();
    }
}