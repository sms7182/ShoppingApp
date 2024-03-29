﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            Id=Guid.NewGuid();
            
        }

        public Guid Id { get; set; }
        public Guid CreatedById { get; set; }
        public Store Store { get; set; }
        public string Code { get; set; }
        public Status Status { get; set; }
        public double TotalPrice { get; set; }
        public double NetPrice { get; set; }
        public double IncPrice { get; set; }
        public double DecPrice { get; set; }
        public DateTime CreationDate { get; set; }

        
        public List<InvoiceItem> InvoiceItems { get; set; }
      
    }
}
