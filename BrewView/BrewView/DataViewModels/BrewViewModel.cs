using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using BrewView.Contracts;
using BrewView.Pages.Brew.Details;
using BrewView.Pages.Brew.Details.Abstractions;
using BrewView.Pages.Brew.List;
using BrewView.Services;
using DIPS.Xamarin.UI.Commands;
using DIPS.Xamarin.UI.Extensions;

namespace BrewView.DataViewModels
{
    public class BrewViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService m_navigationService;
        private string m_largeImageUrl;
        private string m_smallImageUrl;

        public BrewViewModel(INavigationService navigationService)
        {
            m_navigationService = navigationService;
            SelectedCommand = new AsyncCommand<BrewViewModel>(Navigate, OnException);
        }

        private void OnException(Exception obj)
        {
            
        }

        private async Task Navigate(BrewViewModel viewModel)
        {
            await m_navigationService.Push<BrewListPage, BrewDetailsPage, IBrewDetailsViewModel>(async model =>
            {
                model.CurrentBrew = viewModel;
                await model.Load(viewModel.Basic.ProductId);
            });
        }

        public bool IsFavorite { get; set; } = true;
        public BasicViewModel Basic { get; set; }
        public Logistics Logistics { get; set; }
        public Origins Origins { get; set; }
        public Properties Properties { get; set; }
        public Classification Classification { get; set; }
        public Description Description { get; set; }
        public Ingredients Ingredients { get; set; }
        public IList<Price> Prices { get; set; }

        public string SmallImageUrl
        {
            get => m_smallImageUrl;
            set => PropertyChanged.RaiseWhenSet(ref m_smallImageUrl, value);
        }

        public string LargeImageUrl
        {
            get => m_largeImageUrl;
            set => PropertyChanged.RaiseWhenSet(ref m_largeImageUrl, value);
        }


        public IAsyncCommand<BrewViewModel> SelectedCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}