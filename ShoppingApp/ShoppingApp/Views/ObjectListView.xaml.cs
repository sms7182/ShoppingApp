using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            //for (int row = 0; row < 10; row++)
            //{
            //    var button = new Button() {Text = string.Format("{0}", "Remove")};
            //    button.Clicked += OnClicked;
            //    ;
            //    Grid.SetRow(button,row);
            //    this.dataGrid.Children.Add(button);
            //}

        }

        private void OnClicked(object sender, EventArgs e)
        {
            
        }


        private async void btn_clicked(object sender, EventArgs e)
        {
            var scanner = new MobileBarcodeScanner();
            var mobileBarcodeScanningOptions = new MobileBarcodeScanningOptions();
            mobileBarcodeScanningOptions.AutoRotate = true;
            scanner.AutoFocus();
            if (!scanner.IsTorchOn)
            {
                scanner.Torch(true);
            }
            scanner.TopText = "Hold the camera up to  the barcode ";
            scanner.BottomText = "wait for the barcode automatically  scan!";
           
            ZXing.Result result = await scanner.Scan(mobileBarcodeScanningOptions);
            HandleResult(result);
        }

        private void HandleResult(Result result)
        {
            if (result != null)
            {
                this.devgrid.BatchBegin();
                this.devgrid.BatchBegin();
                
                var dataGridItemsSource = this.devgrid.ItemsSource;
                if (dataGridItemsSource == null)
                {
                    this.devgrid.ItemsSource = new BindingList<InvoiceItem>();
                    dataGridItemsSource = this.devgrid.ItemsSource;
                }

                var invoiceItems = ((BindingList<InvoiceItem>) dataGridItemsSource);


                if (invoiceItems.Any(d => d.ItemName == result.Text))
                {

                    for (int i = 0; i < invoiceItems.Count; i++)
                    {
                        if (invoiceItems[i].ItemName == result.Text)
                        {
                            invoiceItems[i].Quantity = invoiceItems[i].Quantity + 1;
                            invoiceItems[i].TotalPrice = (invoiceItems[i].Quantity * invoiceItems[i].UnitPrice);
                            break;

                        }
                    }
                }
                else
                {
                    var invoiceItem = new InvoiceItem();
                    invoiceItem.Quantity = 1;
                    invoiceItem.ItemName = result.Text;
                    invoiceItem.Unit = "Number";
                    invoiceItem.UnitPrice = 750;
                    invoiceItem.TotalPrice = 750;

                    invoiceItems.Add(invoiceItem);

                }

                this.devgrid.ItemsSource = invoiceItems;

                this.devgrid.RefreshData();
               
               this.devgrid.BatchCommit();
                
            }
        }
    }
}