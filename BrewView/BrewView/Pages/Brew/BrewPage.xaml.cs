using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BrewView.Pages.Brew
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrewPage : ContentPage
    {
        public BrewPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is IBrewPageViewModel bindingContext)
            {
                bindingContext.PropertyChanged -= BindingContextOnPropertyChanged;
                bindingContext.PropertyChanged += BindingContextOnPropertyChanged;
            }
        }

        private void BindingContextOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is IBrewPageViewModel context && e.PropertyName == nameof(context.State))
            {
                if (context.State == ViewState.Details)
                {
                    BrewListView.FadeTo(0, 150, Easing.CubicInOut);
                    BrewListView.TranslateTo(Width, 0, 200, Easing.CubicInOut);
                    BrewDetailsView.FadeTo(1, 150, Easing.CubicInOut);
                }
                else
                {
                    BrewListView.FadeTo(1, 150, Easing.CubicInOut);
                    BrewListView.TranslateTo(0, 0, 200, Easing.CubicInOut);
                    BrewDetailsView.FadeTo(0, 150, Easing.CubicInOut);
                }
            }
        }
    }
}