using Xamarin.Forms;

namespace BrewView.Pages.Shared.Actions
{
    public class FadeToAction : TriggerAction<VisualElement>
    {
        public double FadeTo { get; set; }
        public uint Duration { get; set; } = 250;
        public bool ShouldBeVisibleAfter { get; set; }
        public bool ShouldBeVisibleBefore { get; set; }

        protected override async void Invoke(VisualElement sender)
        {
            sender.IsVisible = ShouldBeVisibleBefore;
            await sender.FadeTo(FadeTo, Duration, Easing.CubicInOut);
            sender.IsVisible = ShouldBeVisibleAfter;
        }
    }
}