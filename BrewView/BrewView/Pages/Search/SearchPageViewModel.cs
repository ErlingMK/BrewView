using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BrewView.DataViewModels;
using BrewView.Pages.Brew;
using BrewView.Services;
using BrewView.Services.Abstracts;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using ViewState = BrewView.Pages.Brew.ViewState;

namespace BrewView.Pages.Search
{
    public class SearchPagePageViewModel : ISearchPageViewModel
    {
        private readonly IExceptionHandler m_exceptionHandler;
        private readonly IBrewPageViewModel m_brewPageViewModel;
        private readonly INavigationService m_navigationService;
        private readonly IVinmonopolService m_vinmonopolService;
        private bool m_isBusy;

        public SearchPagePageViewModel(IVinmonopolService vinmonopolService, IExceptionHandler exceptionHandler, IBrewPageViewModel brewPageViewModel, INavigationService navigationService)
        {
            m_vinmonopolService = vinmonopolService;
            m_exceptionHandler = exceptionHandler;
            m_brewPageViewModel = brewPageViewModel;
            m_navigationService = navigationService;

            SearchCommand = new Command<string>(async searchString => await Search(searchString));
            NavigateToDetailsCommand = new Command<BrewViewModel>(NavigateToDetails);
        }

        public bool IsBusy
        {
            get => m_isBusy;
            set => PropertyChanged.RaiseWhenSet(ref m_isBusy, value);
        }

        public ICommand SearchCommand { get; }
        public ObservableCollection<BrewViewModel> Brews { get; } = new ObservableCollection<BrewViewModel>();
        public ICommand NavigateToDetailsCommand { get; }
        public Task Initialize()
        {
            return Task.CompletedTask;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async Task Search(string searchString)
        {
            IsBusy = true;
            Brews.Clear();
            try
            {
                var brews = await m_vinmonopolService.SearchBrews(searchString);
                brews.ForEach(b => Brews.Add(b));
            }
            catch (Exception e)
            {
                m_exceptionHandler.Handle(e);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void NavigateToDetails(BrewViewModel brew)
        {
            m_brewPageViewModel.CurrentBrew = brew;
            m_brewPageViewModel.State = ViewState.Details;
            m_navigationService.SwitchTabbedPage<BrewPage>();
        }
    }
}