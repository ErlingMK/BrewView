using GraphQL.Client.Http;

namespace BrewView.GraphQL
{
    public interface IGraphQLClientFactory
    {
        GraphQLHttpClient GetClient();
    }
}