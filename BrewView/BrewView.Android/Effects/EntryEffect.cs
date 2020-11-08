using System.ComponentModel;
using Android.Content.Res;
using Android.Widget;
using BrewView.Droid.Effects;
using BrewView.Pages.SignIn.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

[assembly: ExportEffect(typeof(EntryEffect), nameof(EntryEffect))]

namespace BrewView.Droid.Effects
{
    public class EntryEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control is EditText editText)
            {
                editText.FocusChange += EditTextOnFocusChange;
            }
        }

        private void EditTextOnFocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (!e.HasFocus && Element.Parent.Parent is PlaceholderEntry entry) entry.OnLostFocus(Element); 
        }

        protected override void OnDetached()
        {
            if (Control is EditText editText)
            {
                editText.FocusChange -= EditTextOnFocusChange;
            }
        }
    }
}