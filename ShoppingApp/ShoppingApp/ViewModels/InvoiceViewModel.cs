using Bit.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using ShoppingApp.Annotations;
using ShoppingBusinessObject;
using Xamarin.Forms;

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
            InvoiceItems=new List<InvoiceItem>();

            ///todo for testing bind
            InvoiceItems.Add(new InvoiceItem()
            {
                ItemName = "7575",
                Quantity = 5,
                UnitPrice = 12,
                Unit = "Number",
                TotalPrice = 60


            });
            //InvoiceItems.Add(new InvoiceItem()
            //{
            //    ItemName = "7070",
            //    Quantity = 7,
            //    UnitPrice = 12,
            //    Unit = "Number",
            //    TotalPrice = 84


            //});
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
