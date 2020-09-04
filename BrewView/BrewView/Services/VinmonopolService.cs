using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BrewView.DataViewModels;
using BrewView.Http;
using BrewView.Services.Abstracts;

namespace BrewView.Services
{
    public class VinmonopolService : IVinmonopolService
    {
        private readonly IRestClient m_restClient;
        private readonly IModelMapper m_modelMapper;
        private readonly IBrewService m_brewService;

        public VinmonopolService(IRestClient restClient, IModelMapper modelMapper, IBrewService brewService)
        {
            m_restClient = restClient;
            m_modelMapper = modelMapper;
            m_brewService = brewService;
        }

        public async Task<List<BrewViewModel>> SearchBrews(string searchString, int maxResults = 100)
        {
            var formatted = searchString.Replace(" ", "_");

            var brews = await m_restClient.Send<List<Brew>>(new HttpRequestBuilder().WithMethod(HttpMethod.Get)
                .WithRequestUri($"{AppConstants.ApiUrl}{AppConstants.ProductDetailsEndPoint}")
                .AddHeader("Ocp-Apim-Subscription-Key", $"{AppConstants.ApiKey}")
                .AddQueryParameter("productShortNameContains", formatted)
                .AddQueryParameter("maxResults", maxResults)
                .Build());

            //TODO: This should change somehow
            foreach (var brew in brews)
            {
                await m_brewService.CreateBrew(brew);
            }

            return brews.Select(b => m_modelMapper.Mapper(b)).ToList();
        }

        public async Task<BrewViewModel> GetBrew(string productId)
        {
            var brew = await m_restClient.Send<Brew>(new HttpRequestBuilder().WithMethod(HttpMethod.Get)
                .WithRequestUri($"{AppConstants.ApiUrl}{AppConstants.ProductDetailsEndPoint}")
                .AddHeader("Ocp-Apim-Subscription-Key", $"{AppConstants.ApiKey}")
                .AddQueryParameter("productShortNameContains", productId)
                .Build());
            return m_modelMapper.Mapper(brew);
        }
    }
}
