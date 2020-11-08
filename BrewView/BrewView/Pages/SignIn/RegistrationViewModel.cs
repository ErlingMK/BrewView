using System;
using System.ComponentModel;
using System.Net.Mail;
using System.Threading.Tasks;
using BrewView.Contracts.User;
using BrewView.Pages.SignIn.Abstractions;
using BrewView.Services;
using BrewView.Services.Account;
using DIPS.Xamarin.UI.Commands;

namespace BrewView.Pages.SignIn
{
    public class RegistrationViewModel : IRegistrationViewModel
    {
        private readonly IAccountService m_accountService;
        private readonly INavigationService m_navigationService;
        private readonly ISignInViewModel m_signInViewModel;
        private string m_email;
        private string m_password;
        private string m_passwordVerification;

        public RegistrationViewModel(IAccountService accountService, INavigationService navigationService,
            ISignInViewModel signInViewModel)
        {
            m_accountService = accountService;
            m_navigationService = navigationService;
            m_signInViewModel = signInViewModel;

            RegisterCommand = new AsyncCommand(Register,
                () => ValidateEmail(Email) && ValidatePassword(Password) &&
                      ValidatePasswordMatch(PasswordVerification));
        }

        public IAsyncCommand RegisterCommand { get; }

        public string PasswordVerification
        {
            get => m_passwordVerification;
            set
            {
                m_passwordVerification = value;
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get => m_password;
            set
            {
                m_password = value;
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        public string Email
        {
            get => m_email;
            set
            {
                m_email = value;
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }


        public Func<string, bool> EmailValidator => ValidateEmail;
        public Func<string, bool> PasswordValidator => ValidatePassword;
        public Func<string, bool> PasswordMatchValidator => ValidatePasswordMatch;

        public event PropertyChangedEventHandler PropertyChanged;

        private bool ValidatePasswordMatch(string arg)
        {
            return arg == Password;
        }

        private bool ValidatePassword(string arg)
        {
            if (string.IsNullOrEmpty(arg)) return false;
            return arg.Length > 6;
        }

        private bool ValidateEmail(string arg)
        {
            try
            {
                var _ = new MailAddress(arg);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private async Task Register()
        {
            m_signInViewModel.IsBusy = true;
            try
            {
                var response = await m_accountService.RegisterUser(new CredentialsModel
                    {Email = Email, Password = Password});

                if (response.Succeeded)
                {
                    await m_navigationService.RemoveSignIn();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                m_signInViewModel.IsBusy = false;
            }
        }
    }
}