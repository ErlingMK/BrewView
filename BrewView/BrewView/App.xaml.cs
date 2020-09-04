using BrewView.Pages;
using BrewView.Pages.Brew;
using BrewView.Pages.Search;
using BrewView.Pages.SignIn;
using LightInject;
using Xamarin.Forms;

namespace BrewView
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var serviceContainer = new ServiceContainer(new ContainerOptions {EnablePropertyInjection = false});
            serviceContainer.Register(fac => this);
            serviceContainer.RegisterFrom<CompositionRoot>();
            serviceContainer.Register(
                fac => new MainPage(fac.GetInstance<ISearchPageViewModel>(), fac.GetInstance<IBrewPageViewModel>(),
                    fac.GetInstance<SearchPage>(), fac.GetInstance<BrewPage>()), new PerContainerLifetime());
            serviceContainer.Register<IServiceContainer>(fac => serviceContainer, new PerContainerLifetime());

            MainPage = new SignInPage {BindingContext = serviceContainer.GetInstance<ISignInViewModel>()};
            //MainPage = serviceContainer.GetInstance<MainPage>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}