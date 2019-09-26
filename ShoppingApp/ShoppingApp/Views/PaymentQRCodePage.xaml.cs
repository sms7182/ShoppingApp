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
        private InvoiceViewModel viewModel;

        public PaymentQRCodePage(InvoiceViewModel invoiceViewModel)
        {
            InitializeComponent();
            viewModel = invoiceViewModel;
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

            var invoiceId = await viewModel.Save();
            if(invoiceId == null)
            {
                await App.Current.MainPage.DisplayAlert("خطا", "دوباره تلاش کنید", "OK");
                this.QRCodeView.BarcodeValue = string.Empty;
                return;

            }

            this.QRCodeView.BarcodeValue = invoiceId.ToString();
        }
    }
}