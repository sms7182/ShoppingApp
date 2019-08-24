using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ShoppingApp.Views;
using Xamarin.Forms;

namespace ShoppingApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture=new CultureInfo("fa-Ir");
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Mobiletxt.Text) || string.IsNullOrWhiteSpace(Passwordtxt.Text))
            {

            }
            else
            {
                Navigation.PushAsync(new HomePage());
            }
        }
    }
}
