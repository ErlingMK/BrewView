using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrewView.Contracts;
using BrewView.DataViewModels;
using BrewView.Services.Abstracts;

namespace BrewView.Services.Mocks
{
    internal class MockBrewService : IBrewService
    {
        private readonly IModelMapper m_mapper;

        public MockBrewService(IModelMapper mapper)
        {
            m_mapper = mapper;
        }

        public Task<BrewViewModel> FindBrew(string gtin)
        {
            throw new NotImplementedException();
        }

        public async Task<BrewViewModel> GetBrew(string productId)
        {
            var brew = await JsonReader.ReadFromJson<Brew>("BrewDetail.json");

            return m_mapper.Mapper(brew);
        }

        public async Task<IList<BrewViewModel>> GetMyBrews()
        {
            var brews = await JsonReader.ReadFromJson<IList<Brew>>("FavoriteBrews.json");

            var viewModels = brews.Select(brew => m_mapper.Mapper(brew)).ToList();

            return viewModels;
        }

        public Task<bool> ToggleFavorite(string productId)
        {
            return Task.FromResult(true);
        }
    }
}
