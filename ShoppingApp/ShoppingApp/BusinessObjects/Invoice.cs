using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace ShoppingBusinessObject
{
    
   public class Invoice
    {
        public Invoice()
        {
            InvoiceItems=new List<InvoiceItem>();
        }
        public UserInfo User { get; set; }
        public Store Store { get; set; }
        public string Code { get; set; }
        public Status Status { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal NetPrice { get; set; }
        public decimal IncPrice { get; set; }
        public decimal DecPrice { get; set; }
        public DateTime CreationDate { get; set; }

        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}
