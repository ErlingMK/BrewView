using System.Net.Http;
using System.Threading.Tasks;
using BrewView.Http;
using BrewView.Models;

namespace BrewView.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRestClient m_restClient;

        public AccountService(IRestClient restClient)
        {
            m_restClient = restClient;
        }

        public async Task<string> SignIn(CredentialsModel credentialsModel)
        {
            return await m_restClient.Send<string>(new HttpRequestBuilder().WithMethod(HttpMethod.Post)
                .WithJsonContent(credentialsModel)
                .WithRequestUri($"{AppConstants.BaseAddress}{AppConstants.SignInEndPoint}")
                .Build());
        }

        public async Task RegisterUser(RegistrationModel registrationModel)
        {
            await m_restClient.Send<string>(new HttpRequestBuilder().WithMethod(HttpMethod.Post)
                .WithJsonContent(registrationModel)
                .WithRequestUri($"{AppConstants.BaseAddress}{AppConstants.SignInEndPoint}")
                .Build());
        }
    }

    public interface IAccountService
    {
        Task<string> SignIn(CredentialsModel credentialsModel);
        Task RegisterUser(RegistrationModel registrationModel);
    }
}