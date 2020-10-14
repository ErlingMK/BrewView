using System;
using System.Threading.Tasks;
using BrewView.Services;
using DIPS.Xamarin.UI.Commands;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace BrewView.Pages.Shared.Scan
{
    public interface IScanViewModel
    {
        IAsyncCommand OpenScanViewCommand { get; }

        event EventHandler<ScanResultEventArgs> ScanResult;
    }

    public class ScanResultEventArgs : EventArgs
    {
        public Result Result { get; set; }
    }

    public class ScanViewModel : IScanViewModel
    {
        private readonly INavigationService m_navigationService;
        private ZXingScannerPage m_scanModal;

        public ScanViewModel(INavigationService navigationService)
        {
            m_navigationService = navigationService;
            OpenScanViewCommand = new AsyncCommand(OpenScanView);
        }

        private async Task OpenScanView()
        {
            m_scanModal = await m_navigationService.PushScanModal();
            m_scanModal.IsScanning = true;
            m_scanModal.OnScanResult += OnScanResult;
        }

        private void OnScanResult(Result result)
        {
            m_scanModal.IsScanning = false;

            if (result != null)
            {
                ScanResult?.Invoke(this, new ScanResultEventArgs {Result = result});
            }

            // If found navigate to detail page/change context of detail page;
            // Else display not found 
            // Then close or resume scan?
            m_scanModal.OnScanResult -= OnScanResult;
        }

        public IAsyncCommand OpenScanViewCommand { get; }
        public event EventHandler<ScanResultEventArgs> ScanResult;
    }
}
