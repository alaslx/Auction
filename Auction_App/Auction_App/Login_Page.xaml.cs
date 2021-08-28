using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Auction_App
{

    public static class AppData
    {
        public static Profile user;
    }

    public class Profile
    {
        public string name;

        public int balance;

        public Profile(string name, int balance)
        {
            this.name = name;

            this.balance = balance;

        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login_Page : ContentPage
    {

        public Login_Page()
        {
            InitializeComponent();
        }


        async public void OnLoginButtonClicked(object sender, EventArgs e)
        {
            bool IsEmailempty = string.IsNullOrEmpty(UsernameEntry.Text);
            bool IsPasswordempty = string.IsNullOrEmpty(PasswordEntry.Text);


            if (IsEmailempty || IsPasswordempty)
            {
                await DisplayAlert("Fail", "Insert Username and Password", "Ok");
            }
            else
            {

                if (UsernameEntry.Text == "abd" && PasswordEntry.Text == "123")
                {
                    var profile = new Profile("ABD", 200);
                    AppData.user = profile;

                    await Navigation.PushAsync(new Auction_Page());

                }
               else if (UsernameEntry.Text == "ali" && PasswordEntry.Text == "123")
                {
                    var profile = new Profile("ali", 250);
                    AppData.user = profile;

                    await Navigation.PushAsync(new Auction_Page());

                }
                else
                    await DisplayAlert("Fail", "Your Username and Password dont match ", "Ok");

            }
        }

        async private void register_button(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register_Page());
        }
    }
}