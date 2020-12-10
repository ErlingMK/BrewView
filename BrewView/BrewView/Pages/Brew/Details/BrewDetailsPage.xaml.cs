using System;
using System.Threading.Tasks;
using BrewView.Pages.Brew.Details.Abstractions;
using BrewView.Pages.Brew.Details.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BrewView.Pages.Brew.Details
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrewDetailsPage : ContentPage
    {
        private int m_currentView;
        private DescriptionView m_descView;
        private NotesView m_notesView;

        public BrewDetailsPage()
        {
            InitializeComponent();
        }

        private async void Menu_OnMenuChanged(object sender, int e)
        {
            await UpdateView(e);
        }

        protected override bool OnBackButtonPressed()
        {
            if (BindingContext is IBrewDetailsViewModel context && context.AddNoteViewModel != null &&
                context.AddNoteViewModel.Show)
            {
                context.AddNoteViewModel.Show = false;
                return true;
            }

            return base.OnBackButtonPressed();
        }

        private async Task UpdateView(int view)
        {
            if (m_currentView == view) return;
            m_currentView = view;
            var currentContent = DynamicContentView.Content;
            await currentContent.FadeTo(0, 200, Easing.CubicInOut);
            currentContent.InputTransparent = true;

            View newContent = view switch
            {
                1 => new DescriptionView
                {
                    TranslationX = Width
                },
                2 => new NotesView
                {
                    TranslationX = -Width
                },
                _ => new ContentView()
            };

            newContent.InputTransparent = false;
            newContent.Opacity = 1;
            DynamicContentView.Content = newContent;
            await newContent.TranslateTo(0, 0, 150, Easing.CubicInOut);
        }

        private void BusyView_OnOverlayTapped(object sender, EventArgs e)
        {
            //TODO: Find out why this isn't firing.
            if (BindingContext is IBrewDetailsViewModel context) context.AddNoteViewModel.Show = false;
        }
    }
}