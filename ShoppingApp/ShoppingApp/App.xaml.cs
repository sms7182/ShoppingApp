using System;
using ShoppingApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingApp
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;

        public App()
        {

            //Xamarin.Forms.DataGrid.DataGridComponent.Init();

            //MainPage = new ObjectListView();
            //{
            //    BindingContext = new ViewModels.InvoiceViewModel();
            //};

            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
            DatabaseLocation = databaseLocation;           

        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            base.OnResume();
            this.MainPage.ForceLayout();
          
            // Handle when your app resumes
        }
    }
}
