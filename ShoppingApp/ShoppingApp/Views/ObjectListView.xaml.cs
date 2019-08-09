using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.ViewModels;
using ShoppingBusinessObject;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Mobile;

namespace ShoppingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ObjectListView : ContentPage
    {
        

        public ObjectListView()
        {
            InitializeComponent();
        }


     

        private async void btn_clicked(object sender, EventArgs e)
        {
            var scanner = new MobileBarcodeScanner();
            
            scanner.TopText = "Hold the camera up to  the barcode ";
            scanner.BottomText = "wait for the barcode automatically  scan!";
            ZXing.Result result = await scanner.Scan();
            HandleResult(result);
        }

        private void HandleResult(Result result)
        {
            if (result != null)
            {
                var dataGridItemsSource = this.dataGrid.ItemsSource;
                if (dataGridItemsSource != null)
                {
                    var invoiceItem = new InvoiceItem();
                    invoiceItem.Quantity = 1;
                    invoiceItem.ItemName = result.Text;
                    invoiceItem.Unit = "Number";
                    invoiceItem.UnitPrice = 750;
                    invoiceItem.TotalPrice = 750;
                    var invoiceItems = dataGridItemsSource.Cast<InvoiceItem>().ToList();
                    invoiceItems.Add(invoiceItem);
                    this.dataGrid.ItemsSource = invoiceItems;
                }
            }
        }
    }
}