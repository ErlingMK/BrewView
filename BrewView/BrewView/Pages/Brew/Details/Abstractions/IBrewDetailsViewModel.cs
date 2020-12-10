using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BrewView.DataViewModels;
using BrewView.Pages.Brew.Details.ViewModels;
using DIPS.Xamarin.UI.Commands;

namespace BrewView.Pages.Brew.Details.Abstractions
{
    public interface IBrewDetailsViewModel : INotifyPropertyChanged
    {
        bool IsBusy { get; }
        BrewViewModel CurrentBrew { get; set; }
        public IAsyncCommand MakeFavoriteCommand { get; }
        ObservableCollection<ChartWithLabel> Charts { get; }
        ObservableCollection<BrewNoteViewModel> BrewNotes { get; }
        ICommand AddNoteCommand { get; }
        Task Load(string productId);
        IAddNoteViewModel AddNoteViewModel { get; }
        bool HasNotes { get; }
    }
}