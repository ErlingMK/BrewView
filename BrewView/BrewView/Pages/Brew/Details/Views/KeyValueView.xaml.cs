using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BrewView.Pages.Brew.Details.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KeyValueView : ContentView
    {
        public KeyValueView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty KeyProperty =
            BindableProperty.Create(nameof(Key), typeof(string), typeof(KeyValueView));

        public string Key
        {
            get => (string) GetValue(KeyProperty);
            set => SetValue(KeyProperty, value);
        }

        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(string), typeof(KeyValueView));

        public string Value
        {
            get => (string) GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
    }
}