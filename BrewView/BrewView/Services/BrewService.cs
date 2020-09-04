using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BrewView.GraphQL;
using BrewView.Http;
using BrewView.Models;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace BrewView.Services
{
    public class BrewService : IBrewService
    {
        private GraphQLHttpClient m_client;

        public BrewService(IGraphQLClientFactory clientFactory)
        {
            m_client = clientFactory.GetClient();
        }

        public async Task<BrewInfo> CreateBrew(Brew brew)
        {
            var brewInfo = new BrewInfo()
            {
                Gtin = brew.Logistics.Barcodes.First(b => b.IsMainGtin).Gtin,
                ProductId = brew.Basic.ProductId
            };

            var graphQlRequest = new GraphQLRequest
            {
                Query = Mutations.CreateBrew,
                Variables = new
                {
                    input = brewInfo
                },
                OperationName = "CreateAndReturnBrew",
            };

            var response = await m_client.SendMutationAsync<BrewInfoResponse>(graphQlRequest);
            return response.Data.CreateBrew;
        }
    }

    public interface IBrewService
    {
        Task<BrewInfo> CreateBrew(Brew brew);

    }
}