using System.ComponentModel;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Commands;

namespace BrewView.Pages.SignIn.Abstractions
{
    public interface ISignInViewModel : INotifyPropertyChanged
    {
        bool IsBusy { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        IAsyncCommand SignInCommand { get; }
        IRegistrationViewModel RegistrationViewModel { get; }
        IAsyncCommand<string> ProviderSignInCommand { get; }
        Task ProviderTokenRequest(string intentDataString);
        Task<bool> IsSignedIn();
    }
}