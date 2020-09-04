using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BrewView.Pages.Brew
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrewDetailsView : ContentView
    {
        public BrewDetailsView()
        {
            InitializeComponent();
        }

        private async void HeartTapped(object sender, EventArgs e)
        {
            await heartLabel.ScaleTo(0.1, 100, Easing.CubicInOut);
            heartLabel.TextColor = heartLabel.TextColor == Color.Red ? Color.LightGray : Color.Red;
            await heartLabel.ScaleTo(1, 100, Easing.SpringOut);
        }
    }
}