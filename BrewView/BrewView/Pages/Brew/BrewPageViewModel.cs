using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using BrewView.DataViewModels;
using BrewView.Pages.Shared;
using BrewView.Pages.Shared.Scan;
using BrewView.Services;
using DIPS.Xamarin.UI.Commands;
using DIPS.Xamarin.UI.Extensions;

namespace BrewView.Pages.Brew
{
    public class BrewPageViewModel : IBrewPageViewModel
    {
        private readonly IBrewService m_brewService;

        private BrewViewModel m_currentBrew;
        private ViewState m_state;

        public BrewPageViewModel(IBrewService brewService, IScanViewModel scanViewModel)
        {
            ScanViewModel = scanViewModel;
            m_brewService = brewService;

            ScanViewModel.ScanResult += OnScanResult;
        }

        private async void OnScanResult(object sender, ScanResultEventArgs e)
        {
            await FindBrew(e.Result.Text);
        }

        public async Task<bool> FindBrew(string gtin)
        {
            var brew = await m_brewService.FindBrewGtin(gtin);

            return brew != null;
        }

        public IScanViewModel ScanViewModel { get; }

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

    public enum ViewState
    {
        List,
        Details
    }
}