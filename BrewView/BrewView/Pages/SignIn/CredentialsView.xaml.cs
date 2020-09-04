using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BrewView.Pages.SignIn
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CredentialsView : ContentView
    {
        public CredentialsView()
        {
            InitializeComponent();
        }

        public event EventHandler RegistrationSelected;

        private void RegisterTapped(object sender, EventArgs e)
        {
            RegistrationSelected?.Invoke(this, EventArgs.Empty);
        }
    }
}