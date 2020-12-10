using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using BrewView.DataViewModels;

namespace BrewView.Pages.Brew.List
{
    public interface IBrewListViewModel : INotifyPropertyChanged
    {
        Task Initialize();
        ObservableCollection<BrewViewModel> MyBrews { get; set; }
        bool IsBusy { get; set; }
    }
}