using System.Net.Http;
using System.Threading.Tasks;

namespace BrewView.Http
{
    public interface IRestClient
    {
        Task<T> Send<T>(HttpRequestMessage message);
    }
}