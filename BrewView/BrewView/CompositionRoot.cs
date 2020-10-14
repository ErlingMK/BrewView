using BrewView.GraphQL;
using BrewView.Http;
using BrewView.Pages;
using BrewView.Pages.Brew;
using BrewView.Pages.Search;
using BrewView.Pages.Shared;
using BrewView.Pages.Shared.Scan;
using BrewView.Pages.SignIn;
using BrewView.Pages.SignIn.Abstractions;
using BrewView.Services;
using BrewView.Services.Abstracts;
using BrewView.Services.Account;
using LightInject;

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

        private void RegisterPages(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register(factory => new MainPage(factory.GetInstance<ISearchPageViewModel>(),
                factory.GetInstance<IBrewPageViewModel>(), factory.GetInstance<SearchPage>(),
                factory.GetInstance<BrewPage>()));
            serviceRegistry.Register(fac => new BrewPage(), new PerContainerLifetime());
            serviceRegistry.Register(fac => new SearchPage(), new PerContainerLifetime());
            serviceRegistry.Register(fac => new SignInPage(){BindingContext = fac.GetInstance<ISignInViewModel>()});
        }

        private void RegisterViewModels(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ISearchPageViewModel, SearchPagePageViewModel>(new PerContainerLifetime());
            serviceRegistry.Register<IBrewPageViewModel, BrewPageViewModel>(new PerContainerLifetime());
            serviceRegistry.Register<ISignInViewModel, SignInViewModel>(new PerContainerLifetime());

            serviceRegistry.Register<IScanViewModel, ScanViewModel>();
        }

        private void RegisterServices(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IHttpClientFactory, HttpClientFactory>(new PerContainerLifetime());
            serviceRegistry.Register<IRestClient, RestClient>();
            serviceRegistry.Register<IGraphQLClientFactory, GraphQLClientFactory>();

            serviceRegistry.Register<IBrewService, BrewService>();
            serviceRegistry.Register<INavigationService, NavigationService>();
            serviceRegistry.Register<IAccountService, AccountService>();
            serviceRegistry.Register<ITokenService, TokenService>(new PerContainerLifetime());
            serviceRegistry.Register<IExceptionHandler, ExceptionHandler>();

            serviceRegistry.Register<IModelMapper, ModelMapper>();
        }
    }
}
