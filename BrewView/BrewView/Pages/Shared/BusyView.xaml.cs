using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BrewView.Pages.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusyView : ContentView
    {
        public static readonly BindableProperty ShowOverlayProperty =
            BindableProperty.Create(nameof(ShowOverlay), typeof(bool), typeof(BusyView), false);

        public BusyView()
        {
            InitializeComponent();
        }

        public bool ShowOverlay
        {
            get => (bool) GetValue(ShowOverlayProperty);
            set => SetValue(ShowOverlayProperty, value);
        }

        public event EventHandler OverlayTapped;

        private void OnOverlayTapped(object sender, EventArgs e)
        {
            OverlayTapped?.Invoke(this, EventArgs.Empty);
        }
    }
}