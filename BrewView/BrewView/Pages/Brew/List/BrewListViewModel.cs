using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Android.Text;
using BrewView.DataViewModels;
using BrewView.Pages.Shared.Scan;
using BrewView.Services;
using DIPS.Xamarin.UI.Commands;
using DIPS.Xamarin.UI.Extensions;
using LightInject;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace BrewView.Pages.Brew.List
{
    public class BrewListViewModel : IBrewListViewModel
    {
        private readonly IServiceContainer m_container;
        private bool m_isBusy;
        private ObservableCollection<BrewViewModel> m_myBrews = new ObservableCollection<BrewViewModel>();

        private ViewState m_state;

        public BrewListViewModel(IServiceContainer container, IScanViewModel scanViewModel)
        {
            m_container = container;
            ScanViewModel = scanViewModel;

            ScanViewModel.ScanResult += OnScanResult;
        }

        public async Task FindBrew(string gtin)
        {
            try
            {
                var brewViewModel = await m_container.GetInstance<IBrewService>().FindBrew(gtin);
                if (brewViewModel == null)
                {
                    //TODO: Show something.
                }
                else
                {
                    //CurrentBrew = brewViewModel;
                    State = ViewState.Details;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public IScanViewModel ScanViewModel { get; }

        public ObservableCollection<BrewViewModel> MyBrews
        {
            get => m_myBrews;
            set => PropertyChanged.RaiseWhenSet(ref m_myBrews, value);
        }

        public bool IsBusy
        {
            get => m_isBusy;
            set => PropertyChanged.RaiseWhenSet(ref m_isBusy, value);
        }

        public async Task Initialize()
        {
            IsBusy = true;
            try
            {
                MyBrews = new ObservableCollection<BrewViewModel>(await m_container.GetInstance<IBrewService>().GetMyBrews());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public ViewState State
        {
            get => m_state;
            set => PropertyChanged.RaiseWhenSet(ref m_state, value, this);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private async void OnScanResult(object sender, ScanResultEventArgs e)
        {
            await FindBrew(e.Result.Text);
        }
    }

    public enum ViewState
    {
        List,
        Details
    }
}