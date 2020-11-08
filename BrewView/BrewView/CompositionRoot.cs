using BrewView.GraphQL;
using BrewView.Http;
using BrewView.Pages;
using BrewView.Pages.Brew.Details;
using BrewView.Pages.Brew.List;
using BrewView.Pages.Search;
using BrewView.Pages.Shared.Scan;
using BrewView.Pages.SignIn;
using BrewView.Pages.SignIn.Abstractions;
using BrewView.Resources.Fonts;
using BrewView.Services;
using BrewView.Services.Abstracts;
using BrewView.Services.Account;
using BrewView.Services.Mocks;
using LightInject;
using Xamarin.Forms;

namespace BrewView
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            RegisterServices(serviceRegistry);
            RegisterViewModels(serviceRegistry);
            RegisterPages(serviceRegistry);
        }

        private MainPage RegisterMainPage(IServiceFactory factory)
        {
            var mainPage = new MainPage();

            var navigationPage = factory.GetInstance<NavigationPage>();
            navigationPage.BarTextColor = Color.Black;
            navigationPage.IconImageSource = new FontImageSource
                {FontFamily = FontIcons.Solid, Glyph = FontIcons.WineBottle};

            var searchPage = factory.GetInstance<SearchPage>();
            searchPage.IconImageSource = new FontImageSource {FontFamily = FontIcons.Solid, Glyph = FontIcons.Search};

            mainPage.Children.Add(navigationPage);
            mainPage.Children.Add(searchPage);

            return mainPage;
        }

        private void RegisterPages(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register(RegisterMainPage, new PerContainerLifetime());

            serviceRegistry.Register(factory => new BrewDetailsPage
                {BindingContext = factory.GetInstance<IBrewDetailsViewModel>()});

            serviceRegistry.Register(factory => new BrewListPage
                {BindingContext = factory.GetInstance<IBrewListViewModel>()});

            serviceRegistry.Register(factory => new NavigationPage(factory.GetInstance<BrewListPage>()),
                new PerContainerLifetime());

            serviceRegistry.Register(fac => new SearchPage {BindingContext = fac.GetInstance<ISearchViewModel>()});

            serviceRegistry.Register(fac => new SignInPage {BindingContext = fac.GetInstance<ISignInViewModel>()});
        }

        private void RegisterViewModels(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ISearchViewModel, SearchViewModel>(new PerContainerLifetime());

            serviceRegistry.Register<ISignInViewModel, SignInViewModel>(new PerContainerLifetime());

            serviceRegistry.Register<IScanViewModel, ScanViewModel>();

            serviceRegistry.Register<IBrewDetailsViewModel, BrewDetailsViewModel>(new PerContainerLifetime());

            serviceRegistry.Register<IBrewListViewModel, BrewListViewModel>(new PerContainerLifetime());
        }

        private void RegisterServices(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IHttpClientFactory, HttpClientFactory>(new PerContainerLifetime());
            serviceRegistry.Register<IRestClient, RestClient>();
            serviceRegistry.Register<IGraphQLClientFactory, GraphQLClientFactory>();

            serviceRegistry.Register<IBrewService>(factory =>
            {
                if (AppSettings.IsDemo) return new MockBrewService(factory.GetInstance<IModelMapper>());

                return new BrewService(factory.GetInstance<IGraphQLClientFactory>(),
                    factory.GetInstance<IModelMapper>());
            });

            serviceRegistry.Register<INavigationService, NavigationService>();
            serviceRegistry.Register<IAccountService, AccountService>();
            serviceRegistry.Register<ITokenService, TokenService>(new PerContainerLifetime());
            serviceRegistry.Register<IExceptionHandler, ExceptionHandler>();

            serviceRegistry.Register<IModelMapper, ModelMapper>();
        }
    }
}