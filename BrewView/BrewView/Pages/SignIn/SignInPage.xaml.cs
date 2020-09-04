using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BrewView.Pages.SignIn
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterTapped(object sender, EventArgs e)
        {
            await CredentialsView.TranslateTo(Width, 0, 150, Easing.CubicInOut);
            RegistrationView.FadeTo(1, 200, Easing.CubicIn);
        }

        private async void OnRegistrationCancelled(object sender, EventArgs e)
        {
            await RegistrationView.FadeTo(0, 100, Easing.CubicIn);
            CredentialsView.TranslateTo(0, 0, 150, Easing.CubicInOut);
        }

        protected override bool OnBackButtonPressed()
        {
            if (RegistrationView.Opacity == 0)
            {
                return base.OnBackButtonPressed();
            }
            else
            {
                OnRegistrationCancelled(null, EventArgs.Empty);
                return true;
            }
        }
    }
}