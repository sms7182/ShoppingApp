using ShoppingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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
            var assembly = typeof(LoginPage);
            //iconImage.Source = ImageSource.FromResource("ShoppingApp.Assets.Image.interface.png", assembly);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //using (var conn = new SQLiteConnection(App.DatabaseLocation))
            //{
            //    conn.CreateTable<SavedUser>();
            //    var savedUserInfo = conn.Table<SavedUser>().FirstOrDefault();
            //    if (savedUserInfo!=null)
            //    {
            //        emailEntry.Text = savedUserInfo.UserName;
            //        passwordEntry.Text = savedUserInfo.Password;
            //        LoginButton_OnClicked(this, new EventArgs());
            //    }
            //}
        }

        private void LoginButton_OnClicked(object sender, EventArgs e)
        {
            var isEmailEmpty = string.IsNullOrWhiteSpace(emailEntry.Text);
            var isPasswordEmpty = string.IsNullOrWhiteSpace(passwordEntry.Text);
            if (isEmailEmpty || isPasswordEmpty)
            {

            }
            else
            {
                //using (var conn = new SQLiteConnection(App.DatabaseLocation))
                //{
                //    conn.CreateTable<SavedUser>();
                //    var savedUserInfo = conn.Table<SavedUser>().FirstOrDefault();
                //    if (savedUserInfo == null)
                //    {
                //        savedUserInfo = new SavedUser();                        
                //    }
                //    savedUserInfo.UserName = emailEntry.Text;
                //    savedUserInfo.Password = passwordEntry.Text;
                //    var rows = conn.Insert(savedUserInfo);
                //}

                Navigation.PushAsync(new HomePage());
            }
        }

        private void SignupButton_OnClicked(object sender, EventArgs e)
        {
            var isEmailEmpty = string.IsNullOrWhiteSpace(emailEntry.Text);
            var isPasswordEmpty = string.IsNullOrWhiteSpace(passwordEntry.Text);
            if (isEmailEmpty || isPasswordEmpty)
            {

            }
            else
            {
                Navigation.PushAsync(new HomePage());
            }

        }
    }
}
