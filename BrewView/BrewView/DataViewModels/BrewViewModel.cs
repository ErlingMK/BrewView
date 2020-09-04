using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using BrewView.Pages.Brew;
using BrewView.Services;
using DIPS.Xamarin.UI.Commands;
using DIPS.Xamarin.UI.Extensions;

namespace BrewView.DataViewModels
{
    public class BrewViewModel : INotifyPropertyChanged
    {
        private string m_largeImageUrl;
        private string m_smallImageUrl;

        public BrewViewModel()
        {
            MakeFavoriteCommand = new AsyncCommand(MakeFavorite);
        }

        public BasicViewModel Basic { get; set; }
        public Logistics Logistics { get; set; }
        public Origins Origins { get; set; }
        public Properties Properties { get; set; }
        public Classification Classification { get; set; }
        public Description Description { get; set; }
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


        public IAsyncCommand MakeFavoriteCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private Task MakeFavorite()
        {
            return Task.CompletedTask;
        }
    }
}