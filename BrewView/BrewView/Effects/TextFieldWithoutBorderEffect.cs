using Xamarin.Forms;

namespace BrewView.Effects
{
    public class TextFieldWithoutBorderEffect : RoutingEffect
    {
        public TextFieldWithoutBorderEffect() : base(
            $"{AppConstants.ResolutionName}.{nameof(TextFieldWithoutBorderEffect)}")
        {
        }
    }
}