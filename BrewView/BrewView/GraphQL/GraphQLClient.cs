using System;
using System.Net.Http;
using System.Net.Http.Headers;
using BrewView.Http;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Xamarin.Essentials;

namespace BrewView.GraphQL
{
    public class GraphQLClientFactory : IGraphQLClientFactory
    {
        private readonly HttpClient m_httpClient;

        public GraphQLClientFactory(IHttpClientFactory clientFactory)
        {
            m_httpClient = clientFactory.CreateClient();
        }

        public GraphQLHttpClient GetClient()
        {
            var graphQlHttpClient = new GraphQLHttpClient(
                new GraphQLHttpClientOptions {EndPoint = new Uri($"{AppConstants.BaseAddress}")},
                new NewtonsoftJsonSerializer(), m_httpClient);
            graphQlHttpClient.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Preferences.Get($"{AppConstants.Jwt}", ""));
            return graphQlHttpClient;
        }
    }
}