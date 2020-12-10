using System.Collections.Generic;
using System.Threading.Tasks;
using BrewView.DataViewModels;
using BrewView.Pages.Brew.Details;
using BrewView.Pages.Brew.Details.ViewModels;

namespace BrewView.Services.Abstracts
{
    public interface IBrewService
    {
        Task<string> FindBrew(string gtin);
        Task<BrewViewModel> GetBrew(string productId);
        Task<IList<BrewViewModel>> GetMyBrews();
        Task<bool> ToggleFavorite(string productId);
        Task<IList<BrewNoteViewModel>> GetBrewNotes(string productId);
        Task<bool> AddNote(BrewNoteViewModel brewNoteViewModel);
    }
}