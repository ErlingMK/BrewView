using BrewView.DataViewModels;

namespace BrewView.Services.Abstracts
{
    public interface IModelMapper
    {
        BasicViewModel Mapper(Basic source);
        BrewViewModel Mapper(Brew source);
    }
}