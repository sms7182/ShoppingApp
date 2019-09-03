using ShoppingApp.ViewModels;
using ShoppingApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritePage : ContentPage
    {
        public FavoritePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var stores = new List<StoreViewModel>() { new StoreViewModel { Id = Guid.NewGuid(), Name = "فروشگاه کوروش", Address = "تهران-خیابان ولیعصر- پلاک 45" },
                new StoreViewModel{ Id = Guid.NewGuid(), Name = "فروشگاه رفاه", Address = "تهران-تجریش-میدان قدس-پلاک 11" } };

            favoriteListView.ItemsSource = stores;
        }

        private void FavoriteListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Xamarin.Forms.DataGrid.DataGridComponent.Init();

            Navigation.PushAsync(new ObjectListView());
        }
      
        private void DeleteButton_Clicked(object sender, System.EventArgs e)
        {
            
        }
    }
}