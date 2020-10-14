using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using BrewView.DataViewModels;
using BrewView.Pages.Shared.Scan;

namespace BrewView.Pages.Brew
{
    public interface IBrewPageViewModel : INotifyPropertyChanged
    {
        ObservableCollection<BrewViewModel> MyBrews { get; }
        BrewViewModel CurrentBrew { get; set; }
        bool IsBusy { get; }
        Task Initialize();
        ViewState State { get; set; }
        Task<bool> FindBrew(string gtin);
        IScanViewModel ScanViewModel { get; }
    }
}