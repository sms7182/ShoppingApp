using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBusinessObject
{
    public class InvoiceItem
    {
        public string Code { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal NetPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal IncPrice { get; set; }
        public decimal DecPrice { get; set; }
        public string ItemNumber { get; set; }
    }
}
