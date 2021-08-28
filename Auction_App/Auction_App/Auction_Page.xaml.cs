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
    public partial class Auction_Page : ContentPage
    {
        public class Item  //  Auction items details
        {
            public string name;
            public int price;
        }

        public string[] itemsNames = new string[] { "Car", "Ball", "Piano", "Phone" }; // Auction Items names


        static Random rand = new Random();

        public Item CreateItem()
        {
            var item = new Item();
            item.name = itemsNames[rand.Next(0, itemsNames.Length)];
            item.price = rand.Next(10, 50);
            return item; // Returnd Auction Item have Random Name and Random Price
        }

        private Item current;
        private bool OwnTheBid;
        private bool Done;
        private int _counter = 10;
        private int _balance = AppData.user.balance;
        private int _amount;
        private int _failMoney;
        private int _wins = 0;


        public Auction_Page()
        {
            InitializeComponent();
            Intialize();
        }


        private void Intialize()
        {

            _counter = 10;
            Done = false;
            current = CreateItem();
            UpdateUI();
            BeginTimer();
            OtherBid();


        }

        void Bid(int amount, bool mine)
        {
            _amount = amount;
          
            current.price += _amount;

            OwnTheBid = mine;
            UpdateUI();
            _counter = 10;


        }

        async private void OtherBid()
        {
            while (!Done)
            {
                await Task.Delay(rand.Next(0, 10000));
                if (OwnTheBid && _counter > 5)
                    Bid(25, false);
            }
        }

        async private void BeginTimer()
        {


            while (_counter > 0)
            {
                Counter1.Text = string.Format("Time Left : {0}", Convert.ToString(_counter));
                _counter--;
                await Task.Delay(1000);


            }

            Done = true;
            HandleResult();

        }

        async void HandleResult()

        {

            if (OwnTheBid)
            {
                await DisplayAlert(null, "You Have Won the Bid !", "Awesome");
                _wins++;
                Won_Items.Text = string.Format("Earnd Items : {0}", _wins);
                OwnTheBid = false;


            }
            else
            {
                await DisplayAlert(null, "You Have Lost the Bid ", "Try Again");
                OwnTheBid = false;
                _balance += _failMoney;
                Balance_label.Text = string.Format("Your Balance is {0} $", _balance );
            }
            _failMoney = 0;
            Intialize();
            
        }

        void UpdateUI()
        {

            Welcome_Label.Text = "Welcome " + AppData.user.name;
            Balance_label.Text = string.Format("Your Balance is {0} $", _balance);
            Won_Items.Text = string.Format("Earnd Items : {0}", _wins);

            MoneyReserve.Text = string.Format("Reserved Money : {0} $", _failMoney);

            item_name.Text = string.Format("Auction item : {0} ", current.name);
            item_price.Text = string.Format("Current price : {0} ", Convert.ToString(current.price));



            if (OwnTheBid) // if you own the bid , the bid button wont be visable
            {
                bid_button.IsVisible = false;
            }
            else
            {
                bid_button.IsVisible = true;

            }

        }





        // Button Handlers :     
        async private void Button_Clicked(object sender, EventArgs e)
        {
            if (_balance <= 0)
            {
                await DisplayAlert("", "You dont have enough Money", "ok");
                money_button.IsVisible = true;

            }
            else
            {
                Bid(25, true);
               
                _failMoney += _amount;
                _balance -= _amount;
                MoneyReserve.Text = string.Format("Reserved Money : {0} $", _failMoney);
                Balance_label.Text = string.Format("Your Balance is {0} $", _balance);
            }

        }

        async private void Money_Button(object sender, EventArgs e)
        {
            var money_loan = await DisplayActionSheet("What do you need :) ", "Cancel", null, "100$", "500$");

            if (money_loan == "100$")
            {
                _balance += 100;
                Balance_label.Text = string.Format("Your Balance is {0} $", _balance);
            }
            else if (money_loan == "500$")
            {
                _balance += 500;
                Balance_label.Text = string.Format("Your Balance is {0} $", _balance);
            }
            money_button.IsVisible = false;

        }
    }
}