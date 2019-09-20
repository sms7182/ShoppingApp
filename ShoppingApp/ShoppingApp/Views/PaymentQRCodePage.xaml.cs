using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShoppingApp.Helpers;
using ShoppingApp.ViewModels;
using ShoppingApp.ViewModels.Contracts;
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
        private static JsonSerializerSettings MicrosoftDateFormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }
        private HttpContent CreateHttpContent(InvoiceInfo content)
        {
            var json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private async void CreateQRCodeFromInvoice()
        {

            //var invoiceId = Service.CreateInvoice(((InvoiceViewModel)this.BindingContext));
            //this.QRCodeView.BarcodeValue =invoiceId.HasValue?invoiceId.Value.ToString():Guid.Empty.ToString();
            //return;
            if (this.BindingContext == null)
            {
                return;
                
            }
            var invoiceViewModel = ((InvoiceViewModel) this.BindingContext);
            var invoiceItems = invoiceViewModel.InvoiceItems;
            var invoice = new InvoiceInfo();
             invoice.Id = Guid.NewGuid();
            invoice.CreatedBy = "09123794709";
            for (int i = 0; i < invoiceItems.Count; i++)
            {
                var temp=new InvoiceInfoLine();
                temp.Id = Guid.NewGuid();
                temp.ItemCode= invoiceItems[i].ItemNumber;
                temp.ItemName = invoiceItems[i].ItemName;
                temp.ItemId = invoiceItems[i].ItemId;
                temp.NetPrice= invoiceItems[i].NetPrice;
                temp.Quantity = invoiceItems[i].Quantity;
                temp.TotalPrice = invoiceItems[i].TotalPrice;
                temp.UnitPrice = invoiceItems[i].UnitPrice;
                
                invoice.InvoiceInfoLines.Add(temp); 
            }

            invoice.Code ="75";
            invoice.CreationDate=DateTime.UtcNow;
            invoice.NetPrice = invoiceItems.Sum(s => s.DecPrice);
            invoice.TotalPrice = invoiceItems.Sum(d => d.TotalPrice);

            using (HttpClient client = new HttpClient())
            {
                try
                {



                    var url = ApiConfiguration.PostInvoiceUrl;
                    var json = JsonConvert.SerializeObject(invoice);
                    var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                    var response = client.PostAsync(url, stringContent).Result;



                }
                catch (Exception e)
                {

                }
            }

            this.QRCodeView.BarcodeValue = invoice.Id.ToString();
        }
    }
}