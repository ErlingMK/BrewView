using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BrewView.Effects
{
    public class EntryEffect : RoutingEffect
    {
        public EntryEffect() : base($"{AppConstants.ResolutionName}.{nameof(EntryEffect)}")
        {
        }
    }
}
