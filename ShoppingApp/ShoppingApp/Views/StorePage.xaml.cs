using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShoppingApp.ViewModels;
using ShoppingApp.Views;

namespace ShoppingApp.Views
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
            var stores = new List<StoreViewModel>() {
                new StoreViewModel{ Name = "سوپرمارکت ماد", Address = "تهران-میدان توحید-خیابان اردشیر-پلاک 77" },
                new StoreViewModel{ Name = "مرکز خرید بزرگ", Address = "تهران-پیروزان-کوچه علیمردانی-نبش آگاهی-پلاک 33" },
                new StoreViewModel { Name = "فروشگاه کوروش", Address = "تهران-خیابان ولیعصر- پلاک 45" },
                new StoreViewModel{ Name = "فروشگاه رفاه", Address = "تهران-تجریش-میدان قدس-پلاک 11" } ,
                new StoreViewModel{ Name = "فروشگاه شهروند", Address = "تهران-تجریش-پلاک 1" }
            };

            storeListView.ItemsSource = stores;
        }

        private void StoreListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Xamarin.Forms.DataGrid.DataGridComponent.Init();

            Navigation.PushAsync(new ObjectListView());
        }

        private void ViewButton_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ViewStorePage());
        }
    }
}