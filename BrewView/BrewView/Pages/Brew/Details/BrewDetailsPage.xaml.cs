using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BrewView.Pages.Brew.Details
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrewDetailsPage : ContentPage
    {
        public BrewDetailsPage()
        {
            InitializeComponent();
        }

        private async void HeartTapped(object sender, EventArgs e)
        {
            await heartLabel.ScaleTo(0.1, 100, Easing.CubicInOut);
            heartLabel.TextColor = heartLabel.TextColor == Color.FromHex("#FD0C69")
                ? Color.White
                : Color.FromHex("#FD0C69");
            await heartLabel.ScaleTo(1, 100, Easing.SpringOut);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            MarkerBoxView.LayoutTo((sender as VisualElement).Bounds, 100, Easing.CubicInOut);
        }
    }
}