using System;
using System.Net.Http;
using System.Threading.Tasks;
using BrewView.Http;
using BrewView.Models;

namespace BrewView.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IRestClient m_restClient;
        private readonly ITokenService m_tokenService;

        public AccountService(IRestClient restClient, ITokenService tokenService)
        {
            m_restClient = restClient;
            m_tokenService = tokenService;
        }


        public async Task<UserValidationResponse> SignIn(CredentialsModel credentials)
        {
            var requestMessage = new HttpRequestBuilder().WithMethod(HttpMethod.Post)
                .WithJsonContent(credentials)
                .WithRequestUri(AppConstants.SignInEndPoint)
                .Build();

            var response = await m_restClient.Send<UserValidationResponse>(requestMessage);
            
            if (response.Succeeded) m_tokenService.StoreTokens(response);

            return response;
        }

        public async Task SignIn(string provider)
        {
            await m_tokenService.ProviderSignIn(provider);
        }


        public async Task<UserValidationResponse> RegisterUser(CredentialsModel credentials)
        {
            var requestMessage = new HttpRequestBuilder().WithMethod(HttpMethod.Post)
                .WithJsonContent(credentials)
                .WithRequestUri(AppConstants.AccountRegistrationEndpoint)
                .Build();

            var response = await m_restClient.Send<UserValidationResponse>(requestMessage);

            if (response.Succeeded) m_tokenService.StoreTokens(response);

            return response;
        }

        public async Task<bool> ProviderTokenRequest(string intentDataString)
        {
            return await m_tokenService.ProviderTokenRequest(intentDataString);
        }
    }
}