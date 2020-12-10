using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using BrewView.DataViewModels;
using BrewView.Services;
using BrewView.Services.Abstracts;
using DIPS.Xamarin.UI.Extensions;
using LightInject;
using Xamarin.Forms;

namespace BrewView.Pages.Brew.List
{
    public class BrewListViewModel : IBrewListViewModel
    {
        private readonly IServiceContainer m_container;
        private bool m_isBusy;
        private ObservableCollection<BrewViewModel> m_myBrews = new ObservableCollection<BrewViewModel>();


        public BrewListViewModel(IServiceContainer container)
        {
            m_container = container;
        }

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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}