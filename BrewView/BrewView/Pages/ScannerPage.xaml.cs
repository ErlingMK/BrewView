using System.Threading.Tasks;
using BrewView.Pages.Shared.Scan;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace BrewView.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerPage : ContentPage
    {
        private ZXingScannerView m_scanner;
        private IScanViewModel m_context;

        public ScannerPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Content = m_scanner = new ZXingScannerView();
            m_scanner.OnScanResult += OnResult;

            await Task.Delay(500);
            m_scanner.IsScanning = true;

            if (BindingContext is IScanViewModel context)
            {
                m_context = context;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            m_scanner.IsScanning = false;
        }

        private async void OnResult(Result result)
        {
            await m_context.ProcessScan(result);
        }
    }
}