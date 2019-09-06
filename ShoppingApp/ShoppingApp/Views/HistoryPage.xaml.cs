using ShoppingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var store = new StoreViewModel { Name = "سوپرمارکت ماد", Address = "تهران-میدان توحید-خیابان اردشیر-پلاک 77" };
            var invoices = new List<InvoiceViewModel>() {
                new InvoiceViewModel{ Store = store,CreationDate = DateTime.Now.AddDays(-1), TotalPrice = 175000, Code="001",Status = ShoppingBusinessObject.Status.Accept,NetPrice = 175000},
                new InvoiceViewModel{ Store = store,CreationDate = DateTime.Now.AddDays(-11), TotalPrice = 2350, Code="002",Status = ShoppingBusinessObject.Status.Accept,NetPrice = 2350}

            };

            invoiceListView.ItemsSource = invoices;
        }

        private void InvoiceListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new ViewInvoicePage());
        }
    }
}