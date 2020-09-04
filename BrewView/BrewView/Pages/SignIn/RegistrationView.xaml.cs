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
    public partial class RegistrationView : ContentView
    {
        public RegistrationView()
        {
            InitializeComponent();
        }

        public event EventHandler RegistrationCancelled;

        private void Button_OnClicked(object sender, EventArgs e)
        {
            RegistrationCancelled?.Invoke(this, EventArgs.Empty);
        }
    }
}