using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BrewView.DataViewModels;

namespace BrewView.Pages.Search
{
    public interface ISearchPageViewModel : INotifyPropertyChanged
    {
        bool IsBusy { get; }
        ICommand SearchCommand { get; }
        ObservableCollection<BrewViewModel> Brews { get; }
        ICommand NavigateToDetailsCommand { get; }
        Task Initialize();

    }
}