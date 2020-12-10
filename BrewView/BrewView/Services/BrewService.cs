using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrewView.Contracts;
using BrewView.DataViewModels;
using BrewView.GraphQL;
using BrewView.Pages.Brew.Details;
using BrewView.Pages.Brew.Details.ViewModels;
using BrewView.Services.Abstracts;
using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;

namespace BrewView.Services
{
    public class BrewService : IBrewService
    {
        private readonly GraphQLHttpClient m_client;
        private readonly IModelMapper m_modelMapper;

        public BrewService(IGraphQLClientFactory clientFactory, IModelMapper modelMapper)
        {
            m_modelMapper = modelMapper;
            m_client = clientFactory.GetClient();
        }


        public async Task<string> FindBrew(string gtin)
        {
            var graphQlRequest = new GraphQLRequest
            {
                Query = Queries.BrewByGtin,
                Variables = new {gtin},
            };

            var response = await m_client.SendQueryAsync(graphQlRequest, () => new {Id = ""});
            return response.Data.Id;
        }

        public async Task<BrewViewModel> GetBrew(string productId)
        {
            var graphQlRequest = new GraphQLRequest()
            {
                Query = Queries.Brew,
                Variables = new {productId}
            };

            var response = await m_client.SendQueryAsync(graphQlRequest, () => new { brew = new Brew() });
            return m_modelMapper.Mapper(response.Data.brew);
        }

        public async Task<IList<BrewViewModel>> GetMyBrews()
        {
            var graphQlRequest = new GraphQLRequest
            {
                Query = Queries.Brews
            };

            var response = await m_client.SendQueryAsync(graphQlRequest, () => new {brews = new List<Brew>()});
            return response.Data.brews.Select(brew => m_modelMapper.Mapper(brew)).ToList();
        }

        public async Task<bool> ToggleFavorite(string productId)
        {
            var graphQlRequest = new GraphQLRequest()
            {
                Query = Mutations.ToggleFavorite,
                Variables = new {productId}
            };

            var response = await m_client.SendMutationAsync(graphQlRequest, () => new {makeFavorite = false});
            return response.Data.makeFavorite;
        }

        public Task<IList<BrewNoteViewModel>> GetBrewNotes(string productId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> AddNote(BrewNoteViewModel brewNoteViewModel)
        {
            throw new System.NotImplementedException();
        }
    }
}