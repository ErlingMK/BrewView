using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using BrewView.DataViewModels;
using BrewView.Services;
using DIPS.Xamarin.UI.Commands;
using DIPS.Xamarin.UI.Extensions;
using LightInject;
using Microcharts;
using SkiaSharp;

namespace BrewView.Pages.Brew.Details
{
    public class BrewDetailsViewModel : IBrewDetailsViewModel
    {
        private readonly IServiceContainer m_serviceContainer;
        private BrewViewModel m_currentBrew;
        private Chart m_descriptionChart;
        private bool m_isBusy;

        public BrewDetailsViewModel(IServiceContainer serviceContainer)
        {
            m_serviceContainer = serviceContainer;
            MakeFavoriteCommand = new AsyncCommand(MakeFavorite);
        }

        public IAsyncCommand MakeFavoriteCommand { get; }

        public bool IsBusy
        {
            get => m_isBusy;
            set => PropertyChanged.RaiseWhenSet(ref m_isBusy, value);
        }


        public async Task Load(BrewViewModel brewViewModel)
        {
            IsBusy = true;
            try
            {
                var brew = await m_serviceContainer.GetInstance<IBrewService>().GetBrew(brewViewModel.Basic.ProductId);
                CurrentBrew = brew;
                CreateCharts();
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

        public ObservableCollection<ChartWithLabel> Charts { get; set; } = new ObservableCollection<ChartWithLabel>();

        public Chart DescriptionChart
        {
            get => m_descriptionChart;
            set => PropertyChanged.RaiseWhenSet(ref m_descriptionChart, value);
        }

        public BrewViewModel CurrentBrew
        {
            get => m_currentBrew;
            set => PropertyChanged.RaiseWhenSet(ref m_currentBrew, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void CreateCharts()
        {
            Charts.Clear();
            CreateDescriptionChart(nameof(CurrentBrew.Description.Fullness));
            CreateDescriptionChart(nameof(CurrentBrew.Description.Sweetness));
            CreateDescriptionChart(nameof(CurrentBrew.Description.Bitterness));
            CreateDescriptionChart(nameof(CurrentBrew.Description.Freshness));
            CreateDescriptionChart(nameof(CurrentBrew.Description.Tannins));
        }

        private void CreateDescriptionChart(string property)
        {
            if (float.TryParse(CurrentBrew.Description.GetPropertyValue(property), out var result))
            {
                var pieChart = new PieChart()
                {
                    MaxValue = 12,
                    Entries = new List<ChartEntry>()
                    {
                        new ChartEntry(result)
                        {
                            Color = SKColor.Parse("#FD0C69")
                        },
                        new ChartEntry(12 - result)
                        {
                            Color = SKColors.WhiteSmoke
                        }
                    }
                };

                Charts.Add(new ChartWithLabel()
                {
                    Description = property,
                    Chart = pieChart,
                    Value = result
                });
            }
        }

        private async Task MakeFavorite()
        {
            try
            {
                var result = await m_serviceContainer.GetInstance<IBrewService>()
                    .ToggleFavorite(CurrentBrew.Basic.ProductId);
                //TODO: Do something with result.
            }
            catch (Exception e)
            {
            }
        }
    }

    public interface IBrewDetailsViewModel : INotifyPropertyChanged
    {
        bool IsBusy { get; }
        BrewViewModel CurrentBrew { get; set; }
        public IAsyncCommand MakeFavoriteCommand { get; }

        Chart DescriptionChart { get; }
        Task Load(BrewViewModel brewViewModel);
        ObservableCollection<ChartWithLabel> Charts { get; }
    }

    public class ChartWithLabel 
    {
        public float Value { get; set; }
        public Chart Chart { get; set; }
        public string Description { get; set; }
    }
}