using System;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using BrewView.Http;
using BrewView.Models;
using BrewView.Services;
using DIPS.Xamarin.UI.Commands;
using DIPS.Xamarin.UI.Extensions;

namespace BrewView.Pages.SignIn
{
    public class SignInViewModel : ISignInViewModel
    {
        private readonly INavigationService m_navigationService;
        private readonly IAccountService m_accountService;
        private readonly IRestClient m_restClient;
        private bool m_isBusy;

        public SignInViewModel(IRestClient restClient, INavigationService navigationService, IAccountService accountService)
        {
            m_restClient = restClient;
            m_navigationService = navigationService;
            m_accountService = accountService;

            RegisterCommand = new AsyncCommand(Register);
            SignInCommand = new AsyncCommand(SignIn);
            DemoCommand = new AsyncCommand(async () =>
            {
                CredentialsModel.Password = "asdasd";
                CredentialsModel.Username = "Testbruker";
                await SignIn();
            });
        }

        public CredentialsModel CredentialsModel { get; } = new CredentialsModel();
        public RegistrationModel RegistrationModel { get; } = new RegistrationModel();

        public bool IsBusy
        {
            get => m_isBusy;
            set => PropertyChanged.RaiseWhenSet(ref m_isBusy, value);
        }

        public IAsyncCommand RegisterCommand { get; }
        public IAsyncCommand SignInCommand { get; }
        public IAsyncCommand DemoCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        private async Task SignIn()
        {
            IsBusy = true;

            try
            {
                var response = await m_accountService.SignIn(CredentialsModel);
                
                Xamarin.Essentials.Preferences.Set(AppConstants.Jwt, response);

                m_navigationService.SwitchMainPage();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task Register()
        {
            IsBusy = true;
            try
            {
                await m_accountService.RegisterUser(RegistrationModel);

                CredentialsModel.Password = RegistrationModel.Password;
                CredentialsModel.Username = RegistrationModel.Username;
                await SignIn();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }

    public interface ISignInViewModel : INotifyPropertyChanged
    {
        CredentialsModel CredentialsModel { get; }
        RegistrationModel RegistrationModel { get; }
        bool IsBusy { get; }
        IAsyncCommand RegisterCommand { get; }
        IAsyncCommand SignInCommand { get; }
        IAsyncCommand DemoCommand { get; }
    }
}