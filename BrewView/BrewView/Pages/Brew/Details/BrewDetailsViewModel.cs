using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BrewView.DataViewModels;
using BrewView.Pages.Brew.Details.Abstractions;
using BrewView.Pages.Brew.Details.ViewModels;
using BrewView.Services.Abstracts;
using DIPS.Xamarin.UI.Commands;
using DIPS.Xamarin.UI.Extensions;
using LightInject;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace BrewView.Pages.Brew.Details
{
    public class BrewDetailsViewModel : IBrewDetailsViewModel
    {
        private readonly IServiceContainer m_serviceContainer;
        private IAddNoteViewModel m_addNoteViewModel;
        private BrewViewModel m_currentBrew;
        private bool m_hasNotes;
        private bool m_isBusy;

        public BrewDetailsViewModel(IServiceContainer serviceContainer)
        {
            m_serviceContainer = serviceContainer;

            MakeFavoriteCommand = new AsyncCommand(MakeFavorite);
            AddNoteCommand = new Command(AddNote);
        }

        public IAsyncCommand MakeFavoriteCommand { get; }

        public bool IsBusy
        {
            get => m_isBusy;
            set => PropertyChanged.RaiseWhenSet(ref m_isBusy, value);
        }


        public ObservableCollection<BrewNoteViewModel> BrewNotes { get; } =
            new ObservableCollection<BrewNoteViewModel>();

        public ICommand AddNoteCommand { get; }

        public async Task Load(string productId)
        {
            IsBusy = true;
            try
            {
                var brewService = m_serviceContainer.GetInstance<IBrewService>();
                var brewNotesTask = brewService.GetBrewNotes(productId);
                var brew = await brewService.GetBrew(productId);
                CurrentBrew = brew;
                CreateCharts();

                var notes = await brewNotesTask;
                notes.ForEach(model => BrewNotes.Add(model));
                HasNotes = notes.Any();
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

        public IAddNoteViewModel AddNoteViewModel
        {
            get => m_addNoteViewModel;
            set => PropertyChanged.RaiseWhenSet(ref m_addNoteViewModel, value);
        }

        public bool HasNotes
        {
            get => m_hasNotes;
            set => PropertyChanged.RaiseWhenSet(ref m_hasNotes, value);
        }

        public ObservableCollection<ChartWithLabel> Charts { get; } = new ObservableCollection<ChartWithLabel>();

        public BrewViewModel CurrentBrew
        {
            get => m_currentBrew;
            set => PropertyChanged.RaiseWhenSet(ref m_currentBrew, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void AddNote()
        {
            AddNoteViewModel = new AddNoteViewModel {OnAddAction = OnAddAction, Show = true};
        }

        private async void OnAddAction(BrewNoteViewModel note)
        {
            try
            {
                var response = await m_serviceContainer.GetInstance<IBrewService>().AddNote(note);
                if (!response) return;
                HasNotes = true;
                BrewNotes.Add(note);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

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
            if (!float.TryParse(CurrentBrew.Description.GetPropertyValue(property), out var result)) return;

            var pieChart = new PieChart
            {
                MaxValue = 12,
                Entries = new List<ChartEntry>
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

            Charts.Add(new ChartWithLabel
            {
                Description = property,
                Chart = pieChart,
                Value = result
            });
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
                Console.WriteLine(e);
            }
        }
    }
}