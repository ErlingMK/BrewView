using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BrewView.Http;
using BrewView.Services.Account;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Xamarin.Essentials;

namespace BrewView.GraphQL
{
    public class GraphQLClientFactory : IGraphQLClientFactory
    {
        private readonly ITokenService m_tokenService;
        private readonly HttpClient m_httpClient;

        public GraphQLClientFactory(IHttpClientFactory clientFactory, ITokenService tokenService)
        {
            m_tokenService = tokenService;
            m_httpClient = clientFactory.CreateGraphQLClient();
        }

        public GraphQLHttpClient GetClient()
        {
            AddToken();
            var graphQlHttpClient = new GraphQLHttpClient(
                new GraphQLHttpClientOptions {EndPoint = new Uri($"/graphql")},
                new NewtonsoftJsonSerializer(), m_httpClient);;
            return graphQlHttpClient;
        }

        private async Task<bool> HasValidToken()
        {
            return await m_tokenService.HasValidTokens();
        }

        private void AddToken()
        {
            m_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", m_tokenService.IdToken);
        }
    }
}