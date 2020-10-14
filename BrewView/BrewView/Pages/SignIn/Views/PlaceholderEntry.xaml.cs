using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BrewView.Pages.SignIn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceholderEntry : ContentView
    {
        public static readonly BindableProperty ReturnCommandProperty =
            BindableProperty.Create(nameof(ReturnCommand), typeof(ICommand), typeof(PlaceholderEntry));

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(PlaceholderEntry), string.Empty);

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(PlaceholderEntry), string.Empty);

        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(PlaceholderEntry), false);

        public static readonly BindableProperty ReturnTypeProperty =
            BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(PlaceholderEntry),
                ReturnType.Default);

        public static readonly BindableProperty ValidatorProperty =
            BindableProperty.Create(nameof(Validator), typeof(Func<string, bool>), typeof(PlaceholderEntry));

        public PlaceholderEntry()
        {
            InitializeComponent();
        }

        public ICommand ReturnCommand
        {
            get => (ICommand) GetValue(ReturnCommandProperty);
            set => SetValue(ReturnCommandProperty, value);
        }

        public string Placeholder
        {
            get => (string) GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public bool IsPassword
        {
            get => (bool) GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public ReturnType ReturnType
        {
            get => (ReturnType) GetValue(ReturnTypeProperty);
            set => SetValue(ReturnTypeProperty, value);
        }

        public Func<string, bool> Validator
        {
            get => (Func<string, bool>) GetValue(ValidatorProperty);
            set => SetValue(ValidatorProperty, value);
        }

        private void InputView_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var isValid = Validator?.Invoke(e.NewTextValue);

            if (isValid.HasValue) entry.BackgroundColor = isValid.Value ? Color.Transparent : Color.FromRgba(200,0,0,.1);
        }

        private void ToggleLabel(bool shouldBeVisible = false)
        {
            label.FadeTo(shouldBeVisible ? 1 : 0, 150, Easing.CubicInOut);
            label.TranslateTo(0, shouldBeVisible ? -10 : 0, 150, Easing.CubicInOut);
        }

        private void Entry_OnCompleted(object sender, EventArgs e)
        {
            if (sender is Entry en && string.IsNullOrEmpty(en.Text)) ToggleLabel();
            else ToggleLabel(true);
        }
    }
}