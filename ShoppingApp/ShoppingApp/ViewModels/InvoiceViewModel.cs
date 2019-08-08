using Bit.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using ShoppingApp.Annotations;
using ShoppingBusinessObject;

namespace ShoppingApp.ViewModels
{
    public class InvoiceViewModel:INotifyPropertyChanged
    {
        #region fields

        private List<InvoiceItem> invoiceItems;
        private InvoiceItem selectedItem;
        #endregion

        public List<InvoiceItem> InvoiceItems
        {
            get { return invoiceItems;}
            set { invoiceItems = value;OnPropertyChanged(nameof(InvoiceItems)); }

        }

        public InvoiceItem SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; }
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
