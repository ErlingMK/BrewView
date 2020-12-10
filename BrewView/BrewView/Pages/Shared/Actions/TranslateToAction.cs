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
        public bool OffScreen { get; set; }
        protected override async void Invoke(VisualElement sender)
        {
            var height = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Height / Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density;
            await Task.Delay(Delay);
            await sender.TranslateTo(X, OffScreen ? height : Y, Duration, Easing.CubicInOut);
        }
    }
}
