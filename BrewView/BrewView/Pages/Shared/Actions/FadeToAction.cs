using Xamarin.Forms;

namespace BrewView.Pages.Shared.Actions
{
    public class FadeToAction : TriggerAction<VisualElement>
    {
        public double FadeTo { get; set; }
        public uint Duration { get; set; } = 250;
        protected override void Invoke(VisualElement sender)
        {
            sender.FadeTo(FadeTo, Duration, Easing.CubicInOut);
        }
    }
}
