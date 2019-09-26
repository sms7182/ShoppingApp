
using ShoppingApp.Helpers;
using ShoppingApp.ViewModels.Contracts;
using ShoppingBusinessObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingApp.ViewModels
{
    public class InvoiceVM
    {
        public InvoiceInfo Invoice { get; set; }
        private Guid InvoiceId;
        public InvoiceVM(Guid id)
        {
            InvoiceId = id;
        }

        public async void GetInvoice()
        {
            Invoice = await InvoiceDB.GetById(InvoiceId);
           
        }
    }

    
}
