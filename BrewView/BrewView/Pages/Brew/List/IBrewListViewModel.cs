using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using BrewView.DataViewModels;
using BrewView.Pages.Shared.Scan;
using DIPS.Xamarin.UI.Commands;

namespace BrewView.Pages.Brew.List
{
    public interface IBrewListViewModel : INotifyPropertyChanged
    {
        Task Initialize();
        ViewState State { get; set; }
        Task FindBrew(string gtin);
        IScanViewModel ScanViewModel { get; }
        ObservableCollection<BrewViewModel> MyBrews { get; set; }
        bool IsBusy { get; set; }
    }
}