using System;
using System.Collections.Generic;
using System.Text;
using BrewView.GraphQL;
using BrewView.Http;
using BrewView.Pages.Brew;
using BrewView.Pages.Search;
using BrewView.Pages.SignIn;
using BrewView.Services;
using BrewView.Services.Abstracts;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
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
            serviceRegistry.Register(fac => new BrewPage(), new PerContainerLifetime());
            serviceRegistry.Register(fac => new SearchPage(), new PerContainerLifetime());
            serviceRegistry.Register(fac => new SignInPage());
        }

        private void RegisterViewModels(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ISearchPageViewModel, SearchPagePageViewModel>(new PerContainerLifetime());
            serviceRegistry.Register<IBrewPageViewModel, BrewPageViewModel>(new PerContainerLifetime());
            serviceRegistry.Register<ISignInViewModel, SignInViewModel>(new PerContainerLifetime());
        }

        private void RegisterServices(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IHttpClientFactory, HttpClientFactory>(new PerContainerLifetime());
            serviceRegistry.Register<IRestClient, RestClient>();
            serviceRegistry.Register<IGraphQLClientFactory, GraphQLClientFactory>();

            serviceRegistry.Register<IVinmonopolService, VinmonopolService>();
            serviceRegistry.Register<IBrewService, BrewService>();
            serviceRegistry.Register<INavigationService, NavigationService>();
            serviceRegistry.Register<IAccountService, AccountService>();
            serviceRegistry.Register<IExceptionHandler, ExceptionHandler>();

            serviceRegistry.Register<IModelMapper, ModelMapper>();
        }
    }
}
