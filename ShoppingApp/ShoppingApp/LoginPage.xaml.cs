using ShoppingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Globalization;
using System.Threading;
using SQLite;
using ShoppingApp.Views;

namespace ShoppingApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fa-Ir");
            NavigationPage.SetHasBackButton(this, false);
            var assembly = typeof(LoginPage);
            //iconImage.Source = ImageSource.FromResource("ShoppingApp.Assets.Image.interface.png", assembly);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (var conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<SavedUser>();
                var savedUserInfo = conn.Table<SavedUser>().FirstOrDefault();
                if (savedUserInfo != null)
                {
                    mobileEntry.Text = savedUserInfo.UserName;
                    passwordEntry.Text = savedUserInfo.Password;
                    LoginButton_OnClicked(this, new EventArgs());
                }
            }
        }

        private void LoginButton_OnClicked(object sender, EventArgs e)
        {
            var isMobileEmpty = string.IsNullOrWhiteSpace(mobileEntry.Text);
            var isPasswordEmpty = string.IsNullOrWhiteSpace(passwordEntry.Text);
            if (isMobileEmpty || isPasswordEmpty)
            {

            }
            else
            {
                using (var conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<SavedUser>();
                    var savedUserInfo = conn.Table<SavedUser>().FirstOrDefault();
                    if (savedUserInfo == null)
                    {
                        savedUserInfo = new SavedUser();
                    }
                    savedUserInfo.UserName = mobileEntry.Text;
                    savedUserInfo.Password = passwordEntry.Text;
                    var rows = conn.Insert(savedUserInfo);
                }

                Navigation.PushAsync(new HomePage());
            }
        }

        private void SignupButton_OnClicked(object sender, EventArgs e)
        {
            var isMobileEmpty = string.IsNullOrWhiteSpace(mobileEntry.Text);
            var isPasswordEmpty = string.IsNullOrWhiteSpace(passwordEntry.Text);
            if (isMobileEmpty || isPasswordEmpty)
            {

            }
            else
            {
                Navigation.PushAsync(new RegistrationPage());
            }

        }
    }
}
