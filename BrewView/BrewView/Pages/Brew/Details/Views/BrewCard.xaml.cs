using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BrewView.Pages.Brew.Details.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrewCard : ContentView
    {
        public BrewCard()
        {
            InitializeComponent();
        }

        private async void HeartTapped(object sender, EventArgs e)
        {
            await HeartLabel.ScaleTo(0.1, 100, Easing.CubicInOut);
            HeartLabel.TextColor = HeartLabel.TextColor == Color.FromHex("#FD0C69")
                ? Color.White
                : Color.FromHex("#FD0C69");
            await HeartLabel.ScaleTo(1, 100, Easing.SpringOut);
        }
    }
}