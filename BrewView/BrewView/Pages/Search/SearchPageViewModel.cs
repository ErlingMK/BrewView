using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BrewView.DataViewModels;
using BrewView.Pages.Brew.List;
using BrewView.Services;
using BrewView.Services.Abstracts;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace BrewView.Pages.Search
{
    public class SearchViewModel : ISearchViewModel
    {
        private readonly IExceptionHandler m_exceptionHandler;
        private readonly IBrewListViewModel m_brewListViewModel;
        private readonly INavigationService m_navigationService;
        private bool m_isBusy;

        public SearchViewModel(IExceptionHandler exceptionHandler, IBrewListViewModel brewListViewModel, INavigationService navigationService)
        {
            m_exceptionHandler = exceptionHandler;
            m_brewListViewModel = brewListViewModel;
            m_navigationService = navigationService;

            SearchCommand = new Command<string>(async searchString => await Search(searchString));
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
    }
}