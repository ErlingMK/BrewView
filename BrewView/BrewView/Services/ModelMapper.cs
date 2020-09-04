using System;
using System.Collections.Generic;
using System.Text;
using BrewView.DataViewModels;
using BrewView.Pages.Brew;
using BrewView.Services.Abstracts;

namespace BrewView.Services
{
    public class ModelMapper : IModelMapper
    {
        private readonly INavigationService m_navigationService;
        private readonly IBrewPageViewModel m_brewPageViewModel;

        public ModelMapper(INavigationService navigationService, IBrewPageViewModel brewPageViewModel)
        {
            m_navigationService = navigationService;
            m_brewPageViewModel = brewPageViewModel;
        }

        public BasicViewModel Mapper(Basic source)
        {
            return new BasicViewModel($"{source.Volume}l", $"{source.AlcoholContent}%", source.ProductLongName);
        }

        public BrewViewModel Mapper(Brew source)
        {
            var smallImageUrl = $"https://bilder.vinmonopolet.no/cache/300x300-0/{source.Basic.ProductId}-1.jpg";
            var largeImageUrl = $"https://bilder.vinmonopolet.no/cache/515x515-0/{source.Basic.ProductId}-1.jpg";

            return new BrewViewModel()
            {
                Basic = Mapper(source.Basic),
                Classification = source.Classification,
                Description = source.Description,
                Logistics = source.Logistics,
                Properties = source.Properties,
                Origins = source.Origins,
                Prices = source.Prices,
                LargeImageUrl = largeImageUrl,
                SmallImageUrl = smallImageUrl
            };
        }
    }
}
