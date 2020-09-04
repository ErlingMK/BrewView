using System.Collections.Generic;
using System.Threading.Tasks;
using BrewView.DataViewModels;

namespace BrewView.Services.Abstracts
{
    public interface IVinmonopolService
    {
        Task<List<BrewViewModel>> SearchBrews(string searchString, int maxResults = 25);
        Task<BrewViewModel> GetBrew(string productId);
    }
}