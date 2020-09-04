using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using BrewView.DataViewModels;
using BrewView.Services;
using DIPS.Xamarin.UI.Extensions;

namespace BrewView.Pages.Brew
{
    public class BrewPageViewModel : IBrewPageViewModel
    {
        private readonly IBrewService m_brewService;

        private BrewViewModel m_currentBrew;
        private ViewState m_state;

        public BrewPageViewModel(IBrewService brewService)
        {
            m_brewService = brewService;
        }

        public ObservableCollection<BrewViewModel> MyBrews { get; } = new ObservableCollection<BrewViewModel>();

        public BrewViewModel CurrentBrew
        {
            get => m_currentBrew;
            set => PropertyChanged.RaiseWhenSet(ref m_currentBrew, value);
        }

        public bool IsBusy { get; }

        public Task Initialize()
        {
            return Task.CompletedTask;
            try
            {
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public ViewState State
        {
            get => m_state;
            set => PropertyChanged.RaiseWhenSet(ref m_state, value, this);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public interface IBrewPageViewModel : INotifyPropertyChanged
    {
        ObservableCollection<BrewViewModel> MyBrews { get; }
        BrewViewModel CurrentBrew { get; set; }
        bool IsBusy { get; }
        Task Initialize();
        ViewState State { get; set; }
    }

    public enum ViewState
    {
        List,
        Details
    }
}