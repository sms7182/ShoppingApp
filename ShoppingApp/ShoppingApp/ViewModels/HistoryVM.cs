using ShoppingApp.Helpers;
using ShoppingApp.ViewModels.Commands;
using ShoppingApp.ViewModels.Contracts;
using ShoppingApp.Views;
using ShoppingBusinessObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ShoppingApp.ViewModels
{
    public class HistoryVM
    {
        public HistoryCommand HistoryCommand { get; set; }
        public ObservableCollection<FlatInvoiceInfo> Invoices { get; set; }
        private FlatInvoiceInfo selectedInvoice;

        public FlatInvoiceInfo SelectedInvoice
        {
            get => selectedInvoice; set
            {
                selectedInvoice = value;
                if (selectedInvoice == null)
                {
                    return;
                }

                HistoryCommand.Execute(selectedInvoice);
                selectedInvoice = null;
            }
        }

        public HistoryVM()
        {
            HistoryCommand = new HistoryCommand(this);
            Invoices = new ObservableCollection<FlatInvoiceInfo>();
        }

        public async void GetInvoices()
        {
            var invoices = await InvoiceDB.Read();
            if (invoices != null)
            {
                Invoices.Clear();
                foreach (var invoice in invoices)
                {
                    Invoices.Add(invoice);
                }
            }
        }

        public async void ViewInvoice(FlatInvoiceInfo selectedInvoice)
        {
            var invoicePage = new ViewInvoicePage();
            var invoiceVM = new InvoiceVM(selectedInvoice.Id);
            invoicePage.BindingContext = invoiceVM;
            await App.Current.MainPage.Navigation.PushAsync(invoicePage);
        }
    }
}
