using BrewView.GraphQL;
using BrewView.Http;
using BrewView.Pages;
using BrewView.Pages.Brew.Details;
using BrewView.Pages.Brew.Details.Abstractions;
using BrewView.Pages.Brew.Details.ViewModels;
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
            var mainPage = new MainPage
            {
                SelectedTabColor = Color.Black,
                BarBackgroundColor = Color.FromHex("#DFF6F6")
            };

            var brewListPage = factory.GetInstance<NavigationPage>(nameof(BrewListPage));
            brewListPage.BarTextColor = Color.Black;
            brewListPage.IconImageSource = new FontImageSource {FontFamily = FontIcons.Solid, Glyph = FontIcons.WineBottle};

            var scannerPage = factory.GetInstance<NavigationPage>(nameof(ScannerPage));
            scannerPage.BarTextColor = Color.Black;
            scannerPage.IconImageSource = new FontImageSource {FontFamily = FontIcons.Solid, Glyph = FontIcons.Camera};

            var searchPage = factory.GetInstance<SearchPage>();
            searchPage.IconImageSource = new FontImageSource { FontFamily = FontIcons.Solid, Glyph = FontIcons.Search };

            mainPage.Children.Add(brewListPage);
            //mainPage.Children.Add(searchPage);
            mainPage.Children.Add(scannerPage);

            return mainPage;
        }

        private void RegisterPages(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register(RegisterMainPage, new PerContainerLifetime());

            serviceRegistry.Register(factory => new BrewDetailsPage());

            serviceRegistry.Register(factory => new BrewListPage
                {BindingContext = factory.GetInstance<IBrewListViewModel>()});

            serviceRegistry.Register(
                factory => new ScannerPage {BindingContext = factory.GetInstance<IScanViewModel>()});

            serviceRegistry.Register(factory => new NavigationPage(factory.GetInstance<BrewListPage>()),
                nameof(BrewListPage), new PerContainerLifetime());

            serviceRegistry.Register(factory => new NavigationPage(factory.GetInstance<ScannerPage>()),
                nameof(ScannerPage), new PerContainerLifetime());

            serviceRegistry.Register(fac => new SearchPage {BindingContext = fac.GetInstance<ISearchViewModel>()});

            serviceRegistry.Register(fac => new SignInPage {BindingContext = fac.GetInstance<ISignInViewModel>()});
        }

        private void RegisterViewModels(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ISearchViewModel, SearchViewModel>(new PerContainerLifetime());

            serviceRegistry.Register<ISignInViewModel, SignInViewModel>(new PerContainerLifetime());

            serviceRegistry.Register<IScanViewModel, ScanViewModel>(new PerContainerLifetime());

            serviceRegistry.Register<IBrewDetailsViewModel, BrewDetailsViewModel>();

            serviceRegistry.Register<IBrewListViewModel, BrewListViewModel>(new PerContainerLifetime());

            serviceRegistry.Register<IAddNoteViewModel, AddNoteViewModel>();
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