
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mobile.DataGrid;
using ShoppingApp.Annotations;
using ShoppingApp.ViewModels.Commands;
using ShoppingApp.Views;
using ShoppingBusinessObject;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;

namespace ShoppingApp.ViewModels
{
    public class InvoiceViewModel:ModelObject
    {
        #region fields

        public UserInfo User { get; set; }
        public Store Store { get; set; }
        public string Code { get; set; }
        public Status Status { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal NetPrice { get; set; }
        public decimal IncPrice { get; set; }
        public decimal DecPrice { get; set; }
        public DateTime CreationDate { get; set; }

        private readonly BindingList<InvoiceItem> invoiceItems;
        #endregion
        public BarcodeCommand BarcodeScanCommand { get; set; }
        public QRCodePaymentCommand QrCodePaymentCommand { get; set; }
        
        public BindingList<InvoiceItem> InvoiceItems
        {
            get { return invoiceItems; }
           

        }


        public InvoiceViewModel()
        {
            BarcodeScanCommand = new BarcodeCommand(this);
            QrCodePaymentCommand=new QRCodePaymentCommand(this);
            invoiceItems=new BindingList<InvoiceItem>();
            InvoiceItems.AllowNew = true;
            
          
        }

        public Invoice Invoice { get; set; }


        public async void Scan()
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

             
                if (InvoiceItems.Any(d => d.ItemName == result.Text))
                {

                    for (int i = 0; i < InvoiceItems.Count; i++)
                    {
                        if (InvoiceItems[i].ItemName == result.Text)
                        {
                            InvoiceItems[i].Quantity = InvoiceItems[i].Quantity + 1;
                            InvoiceItems[i].TotalPrice = (InvoiceItems[i].Quantity * InvoiceItems[i].UnitPrice);
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
                    InvoiceItems.Add(invoiceItem);
                    
                   
                }
                InvoiceItems.ResetBindings();
                
                
            }
        }

        public async void NavigateToQRCodePage()
        {
            var paymentQrCodePage = new PaymentQRCodePage(this);
           await App.Current.MainPage.Navigation.PushAsync(paymentQrCodePage);
        }
    }
}
