using System;
using ShoppingApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingApp
{
    public partial class App : Application
    {
        public App()
        {
            //InitializeComponent();

            //MainPage = new MainPage();
            Xamarin.Forms.DataGrid.DataGridComponent.Init();

            MainPage=new NavigationPage(new MainPage());
            //MainPage = new ObjectListView();
            //{
            //    BindingContext = new ViewModels.InvoiceViewModel();
            //};
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
