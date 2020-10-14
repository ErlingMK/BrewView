using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewView.Pages;
using BrewView.Pages.SignIn;
using LightInject;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

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

        public async Task RemoveSignIn()
        {
            if (!m_app.MainPage.Navigation.ModalStack.Any()) return;
            await m_app.MainPage.Navigation.PopModalAsync();

        }

        public async Task ShowSignIn(bool animate = true)
        {   
            if (m_app.MainPage.Navigation.ModalStack.LastOrDefault() is SignInPage) return;
            await m_app.MainPage.Navigation.PushModalAsync(m_serviceContainer.GetInstance<SignInPage>(), animate);
        }

        public async Task<ZXingScannerPage> PushScanModal(MobileBarcodeScanningOptions options = null)
        {
            options ??= new MobileBarcodeScanningOptions();

            var page = new ZXingScannerPage(options);
            await m_app.MainPage.Navigation.PushModalAsync(page);
            return page;
        }
    }

    public interface INavigationService
    {
        void SwitchTabbedPage<TPage>() where TPage : Page;
        void ShowError(Exception e);
        Task RemoveSignIn();
        Task ShowSignIn(bool animate = true);
        Task<ZXingScannerPage> PushScanModal(MobileBarcodeScanningOptions options = null);
    }
}
