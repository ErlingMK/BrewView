using System;
using System.Threading.Tasks;
using BrewView.Pages.Brew.Details;
using BrewView.Pages.Brew.Details.Abstractions;
using BrewView.Services;
using BrewView.Services.Abstracts;
using LightInject;
using Xamarin.Forms;
using ZXing;

namespace BrewView.Pages.Shared.Scan
{
    public interface IScanViewModel
    {
        Task ProcessScan(Result result);
    }

    public class ScanResultEventArgs : EventArgs
    {
        public Result Result { get; set; }
    }

    public class ScanViewModel : IScanViewModel
    {
        private readonly INavigationService m_navigationService;
        private readonly IServiceContainer m_serviceContainer;

        public ScanViewModel(INavigationService navigationService, IServiceContainer serviceContainer)
        {
            m_navigationService = navigationService;
            m_serviceContainer = serviceContainer;
        }

        public async Task ProcessScan(Result scanResult)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await FindBrew(scanResult?.Text);
            });
        }


        private async Task<bool> FindBrew(string gtin)
        {
            //TODO: Add spinner to scan page.

            if (string.IsNullOrEmpty(gtin)) return false;

            try
            {
                var productId = await m_serviceContainer.GetInstance<IBrewService>().FindBrew(gtin);

                if (!string.IsNullOrEmpty(productId))
                {
                    await m_navigationService.Push<ScannerPage, BrewDetailsPage, IBrewDetailsViewModel>(model => model.Load(productId));
                    return true;
                }
                else
                {
                    //TODO: Display alert?
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return false;
        }
    }
}