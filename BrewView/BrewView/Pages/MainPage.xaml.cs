using System.Threading.Tasks;
using BrewView.Pages.Brew;
using BrewView.Pages.Search;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BrewView.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        private readonly IBrewPageViewModel m_brewPageViewModel;
        private readonly ISearchPageViewModel m_searchPageViewModel;

        public MainPage(ISearchPageViewModel searchPageViewModel, IBrewPageViewModel brewPageViewModel,
            SearchPage searchPage, BrewPage brewPage)
        {
            InitializeComponent();
            searchPage.BindingContext = m_searchPageViewModel = searchPageViewModel;
            brewPage.BindingContext = m_brewPageViewModel = brewPageViewModel;
            Children.Add(brewPage);
            Children.Add(searchPage);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        public async Task Initialize()
        {
            await Task.WhenAll(m_searchPageViewModel.Initialize(), m_brewPageViewModel.Initialize());
        }
    }
}