using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BrewView.Pages.Brew
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrewListView : ContentView
    {
        public BrewListView()
        {
            InitializeComponent();
        }
    }
}