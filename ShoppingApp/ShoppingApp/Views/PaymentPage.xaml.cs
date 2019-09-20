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

namespace ShoppingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentPage : ContentPage
    {
        public InvoiceViewModel InvoiceViewModel;
        public PaymentPage(InvoiceViewModel invoiceViewModel)
        {
            InitializeComponent();
            BindingContext = invoiceViewModel;
          var invoiceId=  Service.CreateInvoice(invoiceViewModel);
        }

        private void OnlinePayment_Clicked(object sender, EventArgs e)
        {
            
        }
     
    }
   
}