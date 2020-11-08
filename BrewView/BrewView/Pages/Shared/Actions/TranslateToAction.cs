using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BrewView.Pages.Shared.Actions
{
    public class TranslateToAction : TriggerAction<VisualElement>
    {
        public double Y { get; set; }
        public double X { get; set; }
        public uint Duration { get; set; } = 250;
        public int Delay { get; set; } = 0;

        protected override async void Invoke(VisualElement sender)
        {
            await Task.Delay(Delay);
            await sender.TranslateTo(X, Y, Duration, Easing.CubicInOut);
        }
    }
}
