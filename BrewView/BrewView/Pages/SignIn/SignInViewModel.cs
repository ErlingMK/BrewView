using System;
using System.ComponentModel;
using System.Threading.Tasks;
using BrewView.Models;
using BrewView.Pages.SignIn.Abstractions;
using BrewView.Services;
using BrewView.Services.Account;
using DIPS.Xamarin.UI.Commands;
using DIPS.Xamarin.UI.Extensions;

namespace BrewView.Pages.SignIn
{
    public class SignInViewModel : ISignInViewModel
    {
        private readonly IAccountService m_accountService;
        private readonly ITokenService m_tokenService;
        private readonly INavigationService m_navigationService;
        private string m_email;
        private bool m_isBusy;
        private string m_password;

        public SignInViewModel(INavigationService navigationService,
            IAccountService accountService, ITokenService tokenService)
        {
            RegistrationViewModel = new RegistrationViewModel(accountService, navigationService, this);
            m_navigationService = navigationService;
            m_accountService = accountService;
            m_tokenService = tokenService;

            SignInCommand = new AsyncCommand(SignIn, () => !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password));
            ProviderSignInCommand = new AsyncCommand<string>(ProviderSignIn);
        }

        private async Task ProviderSignIn(string provider)
        {
            try
            {
                await m_accountService.SignIn(provider);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public IRegistrationViewModel RegistrationViewModel { get; }
        public IAsyncCommand<string> ProviderSignInCommand { get; }
        public async Task ProviderTokenRequest(string intentDataString)
        {
            IsBusy = true;

            try
            {
                if (await m_accountService.ProviderTokenRequest(intentDataString))
                {
                    await m_navigationService.RemoveSignIn();
                }
                else
                {
                    //TODO: Display error
                }
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

        public async Task<bool> IsSignedIn()
        {
            IsBusy = true;
            try
            {
                return await m_tokenService.HasValidTokens();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                IsBusy = false;
            }

            return false;
        }

        public string Password
        {
            get => m_password;
            set
            {
                m_password = value;
                SignInCommand.RaiseCanExecuteChanged();
            }
        }

        public string Email
        {
            get => m_email;
            set
            {
                m_email = value;
                SignInCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsBusy
        {
            get => m_isBusy;
            set => PropertyChanged.RaiseWhenSet(ref m_isBusy, value);
        }

        public IAsyncCommand SignInCommand { get; }


        public event PropertyChangedEventHandler PropertyChanged;


        private async Task SignIn()
        {
            IsBusy = true;

            try
            {
                var response = await m_accountService.SignIn(new CredentialsModel {Email = Email, Password = Password});

                if (response.Succeeded) await m_navigationService.RemoveSignIn();
                else
                {
                    //TODO: Display error
                }
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
}