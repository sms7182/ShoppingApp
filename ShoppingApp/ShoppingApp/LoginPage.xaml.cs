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
using ShoppingBusinessObject;
using ShoppingApp.Helpers;

namespace ShoppingApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class LoginPage : ContentPage
    {
        LoginVM viewModel;
        public LoginPage()
        {
            InitializeComponent();
            viewModel = new LoginVM();
            BindingContext = viewModel;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fa-Ir");
            NavigationPage.SetHasBackButton(this, false);
            var assembly = typeof(LoginPage);
            //iconImage.Source = ImageSource.FromResource("ShoppingApp.Assets.Image.interface.png", assembly);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var savedUserInfo = await UserDB.GetLocalUser();
            if (savedUserInfo != null)
            {                
                mobileEntry.Text = savedUserInfo.UserName;
                passwordEntry.Text = savedUserInfo.Password;
                viewModel.Login();
            }

        }
        
    }
}
