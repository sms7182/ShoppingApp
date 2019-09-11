using ShoppingApp.Helpers;
using ShoppingApp.ViewModels.Commands;
using ShoppingApp.Views;
using ShoppingBusinessObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingApp.ViewModels
{
    public class HistoryVM
    {
        public HistoryCommand HistoryCommand { get; set; }
        public List<Invoice> Invoices { get; set; }
        private Invoice selectedInvoice;

        public Invoice SelectedInvoice
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
            GetInvoices();
        }

        public async void GetInvoices()
        {
            var invoices = await InvoiceDB.Read();
            Invoices = invoices;
        }

        public async void ViewInvoice(Invoice selectedInvoice)
        {
            var invoicePage = new ViewInvoicePage();
            var invoiceVM = new InvoiceVM(selectedInvoice);
            invoicePage.BindingContext = invoiceVM;
            await App.Current.MainPage.Navigation.PushAsync(invoicePage);
        }
    }
}
