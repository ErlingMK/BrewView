using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BrewView.Pages.Brew.Details.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentView
    {
        public Menu()
        {
            InitializeComponent();
        }

        public event EventHandler<int> MenuChanged; 

        private void Button2_OnClicked(object sender, EventArgs e)
        {
            MenuChanged?.Invoke(this, 2);
            MarkerBoxView.LayoutTo((sender as VisualElement).Bounds, 150, Easing.CubicInOut);
        }

        private void Button1_OnClicked(object sender, EventArgs e)
        {
            MenuChanged?.Invoke(this, 1);
            MarkerBoxView.LayoutTo((sender as VisualElement).Bounds, 150, Easing.CubicInOut);
        }
    }
}