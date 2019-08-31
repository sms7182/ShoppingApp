using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.ViewModels;
using ShoppingBusinessObject;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.QrCode;

namespace ShoppingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentQRCodePage : ContentPage
    { 
        

        public PaymentQRCodePage(InvoiceViewModel invoiceViewModel)
        {
            InitializeComponent();
            BindingContext = invoiceViewModel;
            CreateQRCodeFromInvoice();
        }

        private void CreateQRCodeFromInvoice()
        {
            if (this.BindingContext == null)
            {
                return;
                
            }
            var invoiceViewModel = ((InvoiceViewModel) this.BindingContext);
            var invoiceItems = invoiceViewModel.InvoiceItems;
            var invoice = new Invoice();
            invoice.InvoiceItems = invoiceItems.Cast<InvoiceItem>().ToList();
            invoice.Code = "1";
            invoice.CreationDate=DateTime.UtcNow;
            invoice.IncPrice = invoiceItems.Sum(s => s.IncPrice);
            invoice.DecPrice = invoiceItems.Sum(s => s.DecPrice);
            invoice.NetPrice = invoiceItems.Sum(s => s.DecPrice);
            invoice.TotalPrice = invoiceItems.Sum(d => d.TotalPrice);
            invoice.Status = Status.Accept;
            ///todo insert to db 
            //var qrCodeEncodingOptions = new QrCodeEncodingOptions()
            //{
            //    DisableECI = true,
            //    CharacterSet = "UTF-8",
            //    Width = 250,
            //    Height = 250
            //};
            //var barcodeWriter = new BarcodeWriter<Guid>();
            //barcodeWriter.Format = BarcodeFormat.QR_CODE;
            //barcodeWriter.Options = qrCodeEncodingOptions;
            this.QRCodeView.BarcodeValue = invoice.Id.ToString();
        }
    }
}