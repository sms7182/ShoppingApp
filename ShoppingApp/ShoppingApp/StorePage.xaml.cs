using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShoppingApp.ViewModels;
using ShoppingApp.Views;

namespace ShoppingApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StorePage : ContentPage
    {
        public StorePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var stores = new List<StoreViewModel>() { new StoreViewModel { Name = "Store01", Address = "Street 01,block02" },
                new StoreViewModel{ Name = "Store02", Address = "Street 015,block0552" } };

            storeListView.ItemsSource = stores;
        }

        private void StoreListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Xamarin.Forms.DataGrid.DataGridComponent.Init();

            Navigation.PushAsync(new ObjectListView());
        }
    }
}