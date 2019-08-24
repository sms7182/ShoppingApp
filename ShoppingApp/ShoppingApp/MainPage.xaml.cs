using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        private int count = 0;
        private void Button_OnClicked(object sender, EventArgs e)
        {
            count++;
            ((Button) sender).Text = $"your clicked {count} times";
        }
    }
}
