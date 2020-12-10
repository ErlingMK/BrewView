using System.Threading.Tasks;
using Xamarin.Forms;

namespace BrewView.Pages.Shared.Actions
{
    internal class FocusAction : TriggerAction<VisualElement>
    {
        protected override async void Invoke(VisualElement sender)
        {
            await Task.Delay(200);
            sender.Focus();
        }
    }
}