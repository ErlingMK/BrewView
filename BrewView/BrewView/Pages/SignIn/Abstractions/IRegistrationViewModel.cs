using System;
using System.ComponentModel;
using DIPS.Xamarin.UI.Commands;

namespace BrewView.Pages.SignIn.Abstractions
{
    public interface IRegistrationViewModel : INotifyPropertyChanged
    {
        IAsyncCommand RegisterCommand { get; }
        string PasswordVerification { get; set; }
        string Password { get; set; }
        string Email { get; set; }
        Func<string, bool> EmailValidator { get; }
        Func<string, bool> PasswordValidator { get; }
        Func<string, bool> PasswordMatchValidator { get; }
    }
}