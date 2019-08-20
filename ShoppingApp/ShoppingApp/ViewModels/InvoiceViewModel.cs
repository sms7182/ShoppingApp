
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ShoppingApp.Annotations;
using ShoppingBusinessObject;
using Xamarin.Forms;

namespace ShoppingApp.ViewModels
{
    public class InvoiceViewModel
    {
        #region fields

        private readonly BindingList<InvoiceItem> invoiceItems;

        #endregion

        public BindingList<InvoiceItem> InvoiceItems
        {
            get { return invoiceItems; }

        }

       
        public InvoiceViewModel()
        {
            invoiceItems=new BindingList<InvoiceItem>();
           

            ///todo for testing bind
            invoiceItems.Add(new InvoiceItem()
            {
                ItemName = "7575",
                Quantity = 1,
                UnitPrice = 12,
                Unit = "Number",
                TotalPrice = 60


            });
           
        }

        public Invoice Invoice { get; set; }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        #endregion
    }
}
