
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
