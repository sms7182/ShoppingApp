using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShoppingApp.Helpers;
using ShoppingApp.ViewModels;

namespace ShoppingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StorePage : ContentPage
    {
        StoreListVM viewModel;
        public StorePage()
        {
            InitializeComponent();
            viewModel = new StoreListVM();
            BindingContext = viewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();                        
        }       
       
    }
}