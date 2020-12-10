using Android.Widget;
using BrewView.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(TextFieldWithoutBorderEffect), nameof(TextFieldWithoutBorderEffect))]

namespace BrewView.Droid.Effects
{
    public class TextFieldWithoutBorderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control is EditText editText) editText.Background = null;
        }

        protected override void OnDetached()
        {
        }
    }
}