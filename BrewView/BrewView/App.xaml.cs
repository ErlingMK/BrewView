using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using BrewView.Pages;
using BrewView.Pages.Brew;
using BrewView.Pages.Search;
using BrewView.Pages.SignIn;
using BrewView.Pages.SignIn.Abstractions;
using BrewView.Services;
using BrewView.Services.Account;
using LightInject;
using Xamarin.Forms;

namespace BrewView
{
    public partial class App : Application
    {
        private readonly ITokenService m_tokenService;
        private readonly INavigationService m_navigationService;
        private readonly ISignInViewModel m_signInViewModel;

        public App()
        {
            InitializeComponent();

            var serviceContainer = new ServiceContainer(new ContainerOptions {EnablePropertyInjection = false});
            serviceContainer.Register(fac => this);
            serviceContainer.RegisterFrom<CompositionRoot>();
            serviceContainer.Register<IServiceContainer>(fac => serviceContainer, new PerContainerLifetime());

            MainPage = serviceContainer.GetInstance<MainPage>();
            m_tokenService = serviceContainer.GetInstance<ITokenService>();
            m_navigationService = serviceContainer.GetInstance<INavigationService>();
            m_signInViewModel = serviceContainer.GetInstance<ISignInViewModel>();
        }

        protected override async void OnStart()
        {
            await m_navigationService.ShowSignIn();
            if (await m_signInViewModel.IsSignedIn()) await m_navigationService.RemoveSignIn();
        }

        protected override void OnSleep()
        {
        }

        protected override async void OnResume()
        {
            //TODO: Figure out how to handle sign in delays when resuming
            await m_signInViewModel.IsSignedIn();
        }

        public async void HandleOAuthRedirect(string intentDataString)
        {
            await m_signInViewModel.ProviderTokenRequest(intentDataString);
        }
    }
}