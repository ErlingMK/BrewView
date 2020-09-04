using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BrewView.Pages;
using LightInject;
using Xamarin.Forms;

namespace BrewView.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceContainer m_serviceContainer;
        private readonly App m_app;

        public NavigationService(IServiceContainer serviceContainer, App app)
        {
            m_serviceContainer = serviceContainer;
            m_app = app;
        }
        public void SwitchTabbedPage<TPage>() where TPage : Page
        {
            var mainPage = m_serviceContainer.GetInstance<MainPage>();
            var toPage = m_serviceContainer.GetInstance<TPage>();
            mainPage.CurrentPage = toPage;
        }

        public void ShowError(Exception e)
        {
            var mainPage = m_serviceContainer.GetInstance<MainPage>();
            var contentPage = new ContentPage {Content = new Label() {Text = $"{e}\n{e.Message}"}, Title = "Error"};
            if (mainPage.Children.Count == 3) mainPage.Children.RemoveAt(2);
            mainPage.Children.Add(contentPage);
        }

        public async void SwitchMainPage()
        {
            var mainPage = m_serviceContainer.GetInstance<MainPage>();
            var initialize = mainPage.Initialize();
            m_app.MainPage = mainPage;
            await initialize;
        }
    }

    public interface INavigationService
    {
        void SwitchTabbedPage<TPage>() where TPage : Page;
        void ShowError(Exception e);
        void SwitchMainPage();
    }
}
