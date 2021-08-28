using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Auction_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register_Page : ContentPage
    {
        public Register_Page()
        {
            InitializeComponent();
        }
        private void Register_Button(object sender, EventArgs e)
        {



            Navigation.PopAsync();
        }
    }
}
    
