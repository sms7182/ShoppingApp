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

namespace ShoppingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentPage : ContentPage
    {
        public InvoiceViewModel InvoiceViewModel;
        public PaymentPage()
        {
            InitializeComponent();

        }

        private void OnlinePayment_Clicked(object sender, EventArgs e)
        {
            
        }
    }
   
}