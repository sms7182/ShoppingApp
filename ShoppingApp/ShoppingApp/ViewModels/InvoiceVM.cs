
using ShoppingBusinessObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingApp.ViewModels
{
    public class InvoiceVM
    {
        public Invoice Invoice { get; set; }
        public InvoiceVM(Invoice selectedInvoice)
        {
            Invoice= selectedInvoice;
        }
    }

    
}
