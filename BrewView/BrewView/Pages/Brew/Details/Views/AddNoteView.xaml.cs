using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BrewView.Pages.Brew.Details.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNoteView : ContentView
    {
        public AddNoteView()
        {
            InitializeComponent();
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            var width = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Width / Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density;

            return base.OnMeasure(width *.9, heightConstraint);
        }
    }
}