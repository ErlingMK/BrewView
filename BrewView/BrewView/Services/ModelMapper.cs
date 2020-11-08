using BrewView.Contracts;
using BrewView.DataViewModels;
using BrewView.Services.Abstracts;

namespace BrewView.Services
{
    public class ModelMapper : IModelMapper
    {
        private readonly INavigationService m_navigationService;

        public ModelMapper(INavigationService navigationService)
        {
            m_navigationService = navigationService;
        }

        public BasicViewModel Mapper(Basic source)
        {
            return new BasicViewModel($"{source.Volume}l", $"{source.AlcoholContent}%", source.ProductLongName,
                source.ProductId, source.ProductShortName);
        }

        public BrewViewModel Mapper(Brew source)
        {
            var smallImageUrl = $"https://bilder.vinmonopolet.no/cache/300x300-0/{source.Basic.ProductId}-1.jpg";
            var largeImageUrl = $"https://bilder.vinmonopolet.no/cache/515x515-0/{source.Basic.ProductId}-1.jpg";

            return new BrewViewModel(m_navigationService) 
            {
                Basic = Mapper(source.Basic),
                Classification = source.Classification,
                Description = source.Description,
                Logistics = source.Logistics,
                Properties = source.Properties,
                Origins = source.Origins,
                Prices = source.Prices,
                Ingredients = source.Ingredients,
                LargeImageUrl = largeImageUrl,
                SmallImageUrl = smallImageUrl
            };
        }
    }
}