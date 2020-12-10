using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrewView.Contracts;
using BrewView.DataViewModels;
using BrewView.Pages.Brew.Details;
using BrewView.Pages.Brew.Details.ViewModels;
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

        public Task<string> FindBrew(string gtin)
        {
            return Task.FromResult("4827358932");
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

        public async Task<IList<BrewNoteViewModel>> GetBrewNotes(string productId)
        {
            await Task.Delay(2000);
            return new List<BrewNoteViewModel>()
            {
                new BrewNoteViewModel(DateTime.Today.AddDays(-20), 6, "Fette fantastisk!!!"),
                new BrewNoteViewModel(DateTime.Today.AddDays(-100), 5, "Ikke like god denne gangen av en eller annen grunn?\n Neste linje her.\n\nEnda en!"),
                new BrewNoteViewModel(DateTime.Today.AddDays(-2), 4, "Fette fantastisk!!!Fette fantastisk!!!Fette fantastisk!!!Fette fantastisk!!!Fette fantastisk!!!Fette fantastisk!!!Fette fantastisk!!!Fette fantastisk!!!Fette fantastisk!!!Fette fantastisk!!!Fette fantastisk!!!Fette fantastisk!!!Fette fantastisk!!!Fette fantastisk!!!Fette fantastisk!!!Fette fantastisk!!!Fette fantastisk!!!Fette fantastisk!!!"),
                new BrewNoteViewModel(DateTime.Today.AddDays(-203), 5, "Fette fantastisk!!!\nFette fantastisk!!!\nFette fantastisk!!!\nFette fantastisk!!!\nFette fantastisk!!!\nFette fantastisk!!!\n"),
            };
        }

        public Task<bool> AddNote(BrewNoteViewModel brewNoteViewModel)
        {
            return Task.FromResult(true);
        }
    }
}
