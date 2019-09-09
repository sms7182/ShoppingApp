using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShoppingApp.Helpers;

namespace ShoppingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StorePage : ContentPage
    {
        public StorePage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var stores = await StoreDB.Read();
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